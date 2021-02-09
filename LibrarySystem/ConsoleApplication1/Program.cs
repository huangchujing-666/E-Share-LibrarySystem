﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        #region 发送邮件
        static void Main(string[] args)
        {
            Program p = new Program();

            string FromMial = "965906179@qq.com";
            string ToMial = "1294650282@qq.com";
            string AuthorizationCode = "zoporzxqlbdzbfhg";
            // string ReplyTo="";
            // string CCMial="";
           // string File_Path = "C:/Users/Administrator/Desktop/附件：深圳农村商业银行应聘登记表（金融科技专场）.xls";
            string File_Path = "";
            p.SendMail("","",FromMial, ToMial, AuthorizationCode, null, null, File_Path);
        }

        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Body">邮件正文</param>
        /// <param name="FromMial">发件人邮箱</param>
        /// <param name="ToMial">收件人邮箱(多个收件人地址用";"号隔开)</param>
        /// <param name="AuthorizationCode">发件人授权码</param>
        /// <param name="ReplyTo">对方回复邮件时默认的接收地址（不设置也是可以的）</param>
        /// <param name="CCMial">//邮件的抄送者(多个抄送人用";"号隔开)</param>
        /// <param name="File_Path">附件的地址</param>
        public void SendMail(string Subject,string Body,string FromMial, string ToMial, string AuthorizationCode, string ReplyTo, string CCMial, string File_Path)
        {
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage();

                //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
                mailMessage.Priority = MailPriority.Normal;

                //发件人邮箱地址。
                mailMessage.From = new MailAddress(FromMial);

                //收件人邮箱地址。需要群发就写多个
                //拆分邮箱地址
                List<string> ToMiallist = ToMial.Split(';').ToList();
                for (int i = 0; i < ToMiallist.Count; i++)
                {
                    mailMessage.To.Add(new MailAddress(ToMiallist[i]));  //收件人邮箱地址。
                }

                if (ReplyTo == "" || ReplyTo == null)
                {
                    ReplyTo = FromMial;
                }

                //对方回复邮件时默认的接收地址(不设置也是可以的哟)
                mailMessage.ReplyTo = new MailAddress(ReplyTo);

                if (CCMial != "" && CCMial != null)
                {
                    List<string> CCMiallist = ToMial.Split(';').ToList();
                    for (int i = 0; i < CCMiallist.Count; i++)
                    {
                        //邮件的抄送者，支持群发
                        mailMessage.CC.Add(new MailAddress(CCMial));
                    }
                }
                //如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
                mailMessage.SubjectEncoding = Encoding.GetEncoding(936);

                //邮件正文是否是HTML格式
                mailMessage.IsBodyHtml = false;

                //邮件标题。
                mailMessage.Subject = "发送邮件测试";
                //邮件内容。
                mailMessage.Body = "测试群发邮件,以及附件邮件！.....";

                //设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
                if (File_Path != "" && File_Path != null)
                {
                    //将附件添加到邮件
                    mailMessage.Attachments.Add(new Attachment(File_Path));
                    //获取或设置此电子邮件的发送通知。
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                }

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient();
                #region 设置邮件服务器地址

                //在这里我使用的是163邮箱，所以是smtp.163.com，如果你使用的是qq邮箱，那么就是smtp.qq.com。
                // client.Host = "smtp.163.com";
                if (FromMial.Length != 0)
                {
                    //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
                    string[] addressor = FromMial.Trim().Split(new Char[] { '@', '.' });
                    switch (addressor[1])
                    {
                        case "163":
                            client.Host = "smtp.163.com";
                            break;
                        case "126":
                            client.Host = "smtp.126.com";
                            break;
                        case "qq":
                            client.Host = "smtp.qq.com";
                            client.Port = 587;
                            break;
                        case "gmail":
                            client.Host = "smtp.gmail.com";
                            break;
                        case "hotmail":
                            client.Host = "smtp.live.com";//outlook邮箱
                            //client.Port = 587;
                            break;
                        case "foxmail":
                            client.Host = "smtp.foxmail.com";
                            break;
                        case "sina":
                            client.Host = "smtp.sina.com.cn";
                            break;
                        default:
                            client.Host = "smtp.exmail.qq.com";//qq企业邮箱
                            break;
                    }
                }
                #endregion

                //使用安全加密连接。
                client.EnableSsl = true;
                //不和请求一块发送。
                client.UseDefaultCredentials = false;

                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                client.Credentials = new NetworkCredential(FromMial, AuthorizationCode);

                //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
              
                //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                //发送
                client.Send(mailMessage);
                Console.WriteLine("发送成功");
            }
            catch (Exception ex)
            {

                Console.WriteLine("发送失败"+ex.Message);
                Console.ReadKey();
            }
        }
        #endregion
    }
}
