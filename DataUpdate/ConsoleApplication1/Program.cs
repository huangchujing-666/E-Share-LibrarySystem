using DataUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dd = new Dictionary<string, string>() { 
               {"7","server=.;database=GPNULibrary;uid=sa;pwd=123456"},
               {"6","server=.;database=SYSULibrary;uid=sa;pwd=123456"},
               {"2","server=.;database=SCUTLibrary;uid=sa;pwd=123456"},
               {"1","server=.;database=SCNULibrary;uid=sa;pwd=123456"}
            };

            string connStr = dd[args[0]];//第一个是学校id
            int Count = int.Parse(args[1]);//第二个是数量

            Random r = new Random();
            r.Next(9000, 10000);
            string s = string.Empty;

            //isbn
            List<string> isbn = new List<string>();
            for (int i = 0; i < Count; i++)
            {
                s = r.Next(9000, 10000).ToString() + "-" + r.Next(9000, 10000).ToString() + "-" + r.Next(9000, 10000).ToString();
                if (!isbn.Contains(s))
                {
                    isbn.Add(s);
                }
            }
            //作者
            List<string> Author = new List<string>() { "黄德群", "吴正江", "王国华", "张铭", "王腾蛟", "赵海燕", "李未", "宁洪", "刘强", "孙吉贵", "庄越挺", "何炎祥", "何钦铭", "张晨曦", "李宣东", "李晓明", "陈钟", "陈道序", "周立序", "周傲英", "孟祥旭", "岳丽华", "罗军舟", "姚淑珍", "胡事民", "骆斌", "徐宝文", "黄虎杰", "蒋建伟", "廖明宏", "熊璋", "樊晓桠", "齐治昌", "陈平", "马殿富" };

            //出版时间
            List<string> PublishDate = new List<string>();
            for (int i = 1900; i <= 2019; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    PublishDate.Add(i + "年" + j + "月");
                }
            }
            //书名
            List<string> bookName = new List<string>() { 
             "数据结构与算法基础","C语言入门","教育技术学","普通话基本教程","算法入门到精通","计算机英语","java入门到精通","java进阶","C#入门与基础","C#从入门到精通","MVC 4.0","Sql server 2008","Sql server 2012","大型数据库技术","教育学","心理学","软件工程","单片机原理","软件测试","JavaEE 2.0","嵌入式系统训练","Web应用开发","信息组织与检索","文化创意与策划","编辑出版实务","新媒体写作","液压与气压传动","材料成型与设备方法"
            };

            //类别
            List<string> category = new List<string>() { 
            "教育类",
            "计算机类",
            "语言类",
            "机电类"
            };
            string bookName1 = string.Empty;
            string publishDate1 = string.Empty;
            string isbn1 = string.Empty;
            string author1 = string.Empty;
            string category1 = string.Empty;
            int aviliable = 0;
            int count = 0;
            for (int i = 0; i < isbn.Count; i++)
            {
                bookName1 = bookName[r.Next(0, bookName.Count - 1)]+"2";
                publishDate1 = PublishDate[r.Next(0, PublishDate.Count - 1)];
                isbn1 = isbn[i];
                author1 = Author[r.Next(0, Author.Count - 1)];
                category1 = category[r.Next(0, category.Count - 1)];

                count = r.Next(10, 100);
                aviliable = count - 2;
                string sqlstr = "insert into BookInfo(Title,Isbn,Category,Author,PublicDate,Count,Available) values('" + bookName1 + "','" + isbn1 + "','" + category1 + "','" + author1 + "','" + publishDate1 + "'," + count + "," + aviliable+")";

                SqlHelper.ExecuteNonQuery(sqlstr, connStr);

                 bookName1 = string.Empty;
                 publishDate1 = string.Empty;
                 isbn1 = string.Empty;
                 author1 = string.Empty;
                 category1 = string.Empty;
                 aviliable = 0;
                 count = 0;
            }
        }
    }
}
