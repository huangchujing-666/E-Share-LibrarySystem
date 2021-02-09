using DataUpdate.Dal;
using DataUpdate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataUpdate
{
    class Program
    {
        /// <summary>
        /// 已经开启的线程数
        /// </summary>
        static int threadCount = 0;
        /// <summary>
        /// 记录已经更新完的总数
        /// </summary>
        static int count = 0;

        static bool IsDelete = false;
        static void Main(string[] args)//  args[0]=1 DataSourceConfig=1 args[1]=true 是否多次更新=true args[2]=操作人Id
        {
            Console.WriteLine("args[0]:" + args[0]+"---------args[1]:"+args[1]+"----------args[2]:"+args[2]);
            int SysAccountId = int.Parse(args[2]);
           // Console.ReadKey();
            if (SysAccountId > 0)
            {
                CommHelp.SysAccountId = SysAccountId;
            }
            DataSourceConfigDal dataSourceDal = new DataSourceConfigDal();
            University sourceConfigu = new University();
            ViewConfigDal vdal = new ViewConfigDal();
            ErrorLogDal errorlogDal = new ErrorLogDal();
            OriginDataDal odDal = new OriginDataDal();
            UniversityDal uDal = new UniversityDal();
            //args[0]=1 DataSourceConfig=1 args[1]=true 是否多次更新=true
            Console.WriteLine("本地数据库配置信息：" + CommHelp.ConnStr);
            //1.0 获取数据源配置信息，尝试连接数据源数据库
            if (CommHelp.IsFirstUpdate)
            {
                CommHelp.IsFirstUpdate = false;
                if (args.Length > 0 && args[0] != null)
                {
                    //1.1 获取数据源配置信息
                    sourceConfigu = dataSourceDal.GetDataSourceConfig(args[0]);
                    if (sourceConfigu != null)
                    {
                        CommHelp.University = sourceConfigu;
                        Console.WriteLine("获取数据源信息成功！" + (bool.Parse(args[1]) ? "更新多次" : "更新一次"));
                    }
                       
                    else
                    {
                        Console.WriteLine("获取数据数据源信息失败！");
                        return;
                    }
                    var viewconfig = vdal.GetViewConfig();
                    if (viewconfig != null)
                    {
                        CommHelp.viewConfig = viewconfig;
                        Console.WriteLine("获取数据源视图配置信息成功");
                    }
                    else
                    {
                        Console.WriteLine("获取数据源视图配置信息成功");
                        return;
                    }
                    //1.2 尝试连接数据源
                    Exception ex;
                    CommHelp.DataSourceConn = "server=" + sourceConfigu.Service + ";database=" + sourceConfigu.DataBase + ";uid=" + sourceConfigu.UserId + ";pwd=" + sourceConfigu.UserPwd;
                    if (SqlHelper.ConnTest(CommHelp.DataSourceConn, out ex))
                    {
                        Console.WriteLine("数据库连接测试成功!");
                        CommHelp.SleepTime = sourceConfigu.TimeStart;
                        Console.WriteLine("数据源配置信息：" + CommHelp.DataSourceConn);
                    }
                }
                else
                {
                    Console.WriteLine("获取数据源信息失败！");
                    return;
                }
            }
            //2.0 是否为单次更新
            CommHelp.IsRepetitionUpdate = bool.Parse(args[1]);

            //3.0 修改控制台标题
            Console.Title = "更新数据库原始数据信息表" + CommHelp.University.Service + ((args[1] == "true") ? "多次更新" : " 单次");

            //4.0 获取当前供应商下为第几次更新数据
            if (errorlogDal.GetUpdateIndex(CommHelp.University.UniversityId, CommHelp.SysAccountId, out CommHelp.UpdateIndex))
            {
                //删除本次有关错误日志数据
                //errorlogDal.DeleteErrorDal(sourceConfigu.Id, CommHelp.UpdateIndex);
                Console.WriteLine("第" + CommHelp.UpdateIndex + "次更新开始");
                Console.WriteLine("开始时间：" + System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            }
            else
            {
                Console.WriteLine("获取当前数据源为第几次更新失败！");
                return;
            }
            Console.WriteLine("当前数据源操作下第" + CommHelp.UpdateIndex + "次更新数据中...请勿关闭！");


            //5.0 开始进行数据更新
            string mess = string.Empty;
            int k = odDal.GetDataSourceCount(CommHelp.University.UniversityId, "", CommHelp.DataSourceConn, out mess);
            if (k == 0)
            {
                while (true)
                {
                    //如果取不到远程服务器上则递归调用，直到取到数据为止
                    k = k = odDal.GetDataSourceCount(CommHelp.University.UniversityId, "", CommHelp.DataSourceConn, out mess);
                    if (k == 0)
                    {
                        Console.WriteLine(mess);
                        Console.WriteLine("正在重新请求！");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            k = (k % CommHelp.UpdateCount == 0 ? k / CommHelp.UpdateCount : k / CommHelp.UpdateCount + 1);
            //6.0 查询错误日志表，对更新失败数据进行二次更新

            //5.2 查询本地数据库中存在，在服务器中不存在的数据，并且禁用操作
            Thread thread1 = new Thread(delegate(object obj)
            {
                IsDelete = true;
                uDal.DelteClientBookInfo(uDal.GetDeleteClientDataList(CommHelp.viewConfig));
                IsDelete = false;
            });
            thread1.IsBackground = true;
            thread1.Start();
            int j = 0;

            int Process = 0;
            int CalculateCount = 0;

            ////5.3 从远程服务器每次获取100条数据，并且更新本地数据库(更新操作)
            ////当前更新进度（Process）小于所需更新次数（CalculateCount）继续启用线程同步数据
            //while (Process < CalculateCount)
            //{
            //    //当启用的线程数量（threadCount）小于规定的线程数量（CommHelp.ThreadCount=5）
            //    //且此次更新进度为完结时继续启用线程
            //    while (threadCount < CommHelp.ThreadCount && Process < CalculateCount)
            //    {
            //        //启用线程数自增
            //        threadCount++;
            //        //开启新线程
            //        Thread thread = new Thread(TH);
            //        //设置为后台线程
            //        thread.IsBackground = true;
            //        //msg对象实例用户记录线程号和错误信息和更新进度
            //        MSG msg = new MSG();
            //        msg.i = Process;
            //        //启用线程
            //        thread.Start(msg);
            //        //进度自增
            //        Process++;
            //    }
            //    //线程休眠100毫秒，错开并发执行时间点
            //    Thread.Sleep(100);
            //}

            //5.3 从远程服务器每次获取100条数据，并且更新本地数据库(更新操作)
            while (j < k)
            {
                while (threadCount < CommHelp.ThreadCount && j < k)
                {
                    threadCount++;
                    Thread thread = new Thread(TH);
                    thread.IsBackground = true;
                    MSG msg = new MSG();
                    msg.i = j;
                    thread.Start(msg);
                    j++;
                }
                Thread.Sleep(100);
            }


            #region MyRegion
            //6.当第5步更新完毕之后，到错误日志表查询更新错误的记录
            while (true)
            {   //当前开启的线程数为0且删除数据完成
                if (threadCount == 0 && !IsDelete)
                {   //重新开启线程对本次更新失败的数据再次判断进行更新
                    Thread thread = new Thread(NewRun);
                    //设置为后台线程
                    thread.IsBackground = false;
                    //线程开始
                    thread.Start();
                    //等待线程执行完毕
                    thread.Join();
                    Console.WriteLine("结束时间:" + DateTime.Now);
                    #region 本次更新添加结束时间记录到日子表中
                    errorlogDal.UpdateErrorLog(new ErrorLog()
                    {
                        ErrorLogId = CommHelp.ErrorLogId,
                        SysAccountId = CommHelp.SysAccountId,
                        UniversityId = CommHelp.SourceConfigId,
                        UpdateCount = CommHelp.UpdateIndex,
                        Type = (int)EnumHelp.ErrorLogType.更新记录,
                        EndTime = System.DateTime.Now,
                        Status = (int)EnumHelp.UpdateStatus.更新成功
                    });
                    #endregion
                    count = 0;
                    //判断是否多次更新
                    if (CommHelp.IsRepetitionUpdate)
                    {   //如是
                        //线程休眠
                        Thread.Sleep(CommHelp.SleepTime*1000);
                        //调用程序入口，进行下一轮执行
                        Main(args);       }
                    else
                    {   //否则，更新数据源启用状态字段
                        SqlHelper.ExecuteNonQuery("Update University set IsUpdate=" + (int)EnumHelp.UpdateStartEnum.未启用 + " where UniversityId=" + CommHelp.SourceConfigId);
                     }
                    return;//结束程序
                }
            }
            #endregion

        }



        #region 传入线程的方法+void TH(object obj)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Msg</param>
        private static void TH(object obj)
        {
            MSG msg = obj as MSG;
            DataSourceConfigDal dataSourceDal = new DataSourceConfigDal();
            string message;
            if (!dataSourceDal.UpdateClientDataTables(msg.i * CommHelp.UpdateCount, (msg.i + 1) * CommHelp.UpdateCount, out message))
            {
                Console.WriteLine("错误：" + (count + CommHelp.UpdateCount) + "条，" + DateTime.Now.ToString() + "第：" + (msg.i + 1) * CommHelp.UpdateCount + "行添加失败，失败原因：" + message);
            }
            else
            {
                Console.WriteLine("已更新：" + (count += CommHelp.UpdateCount) + "条，" + DateTime.Now.ToString() + ":第" + (msg.i * CommHelp.UpdateCount + 1) + "-" + (msg.i + 1) * CommHelp.UpdateCount + "条数据更新成功");
                Console.WriteLine("正在更新数据库中，请勿关闭...");
            }
            threadCount--;
        }
        #endregion


        /// <summary>
        /// 回调次数
        /// </summary>
        static int NewRunIndex = 0;

        /// <summary>
        /// 再次更新
        /// </summary>
        static void NewRun()
        {
            DataSourceConfigDal dataSourceDal = new DataSourceConfigDal();
            string mess;
            List<int[]> list = dataSourceDal.AgainRenew(CommHelp.SourceConfigId,CommHelp.UpdateIndex, out mess);
            if (list == null)
            {
                Console.WriteLine(mess);
                if (NewRunIndex < 3)//回调3次
                {
                    NewRunIndex++;
                    NewRun();
                }
                else
                {
                    Console.WriteLine("获取本次更新失败的数据再次重新更新这些数据失败！");
                }
            }
            else
            {
                int j = 0;
                int k = list.Count;
                while (j < k)
                {
                    if (k <= CommHelp.ThreadCount )
                    {
                        while (threadCount <= k)//CommHelp.ThreadCount
                        {
                            threadCount++;
                            Thread thread = new Thread(NewTH);
                            thread.IsBackground = true;
                            MSG msg = new MSG();
                            msg.i = j;
                            msg.list = list;
                            thread.Start(msg);
                            j++;
                        }
                    }
                    else
                    {
                        while (threadCount < CommHelp.ThreadCount && j < k)
                        {
                            threadCount++;
                            Thread thread = new Thread(NewTH);
                            thread.IsBackground = true;
                            MSG msg = new MSG();
                            msg.i = j;
                            msg.list = list;
                            thread.Start(msg);
                            j++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 第二次更新
        /// </summary>
        /// <param name="obj"></param>
        static void NewTH(object obj)
        {
            DataSourceConfigDal dataSourceDal = new DataSourceConfigDal();
            MSG msg = obj as MSG;
            string mess;
            if (!dataSourceDal.UpdateClientDataTables(msg.list[msg.i][0], msg.list[msg.i][1], out mess))
            {
                Console.WriteLine("错误：" + (count + CommHelp.UpdateCount) + "条，" + DateTime.Now.ToString() + ":第" + msg.list[msg.i][0] + "行添加失败！\r\n 错误详情：" + mess);
            }
            else
            {
                Console.WriteLine("已更新：" + (count += CommHelp.UpdateCount) + "条，" + DateTime.Now.ToString() + ":第" + msg.list[msg.i][0] + "-" + msg.list[msg.i][1] + "条更新数据成功！");
                Console.WriteLine("更新数据库中....请勿关闭！");
            }
            threadCount--;
        }
    }
}
