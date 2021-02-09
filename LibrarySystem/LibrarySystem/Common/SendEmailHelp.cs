using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace LibrarySystem.Common
{
    public static class SendEmailHelp
    {
        private static string FromMial = "965906179@qq.com";
        private static string AuthorizationCode = "zoporzxqlbdzbfhg";

        #region  发送邮件方法
        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="Subject">邮件主题</param>
        /// <param name="Body">邮件正文</param>
        /// <param name="ToMial">收件人邮箱(多个收件人地址用";"号隔开)</param>
        /// <param name="ReplyTo">对方回复邮件时默认的接收地址（不设置也是可以的）</param>
        /// <param name="CCMial">//邮件的抄送者(多个抄送人用";"号隔开)</param>
        /// <param name="File_Path">附件的地址</param>
        /// <param name="FromMial">发件人邮箱</param>
        /// <param name="AuthorizationCode">发件人授权码  需要到QQ邮箱去授权验证</param>
        public static bool SendMail(string Subject, string Body, string ToMial, string ReplyTo, string CCMial, string File_Path, string FromMial = "965906179@qq.com", string AuthorizationCode = "zoporzxqlbdzbfhg")
        {
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.EnableSsl = true;
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.Host = "smtp.qq.com";
            _smtpClient.Port = 587;
            _smtpClient.Credentials = new System.Net.NetworkCredential(FromMial, AuthorizationCode);
            //密码不是QQ密码，是qq账户设置里面的POP3/SMTP服务生成的key

            MailMessage _mailMessage = new MailMessage(FromMial, ToMial);
            _mailMessage.Subject = Subject;//主题 
            _mailMessage.Body = Body;//内容
            _mailMessage.BodyEncoding = Encoding.Default;//正文编码 
            _mailMessage.IsBodyHtml = true;//设置为HTML格式 
            _mailMessage.Priority = MailPriority.High;//优先级 

            try
            {
                _smtpClient.Send(_mailMessage);
                Console.WriteLine("发送成功");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("发送失败");
                throw e;
            }
            //try
            //{
            //    //1. 实例化一个发送邮件类
            //    MailMessage mailMessage = new MailMessage();

            //    //2. 设置邮件的优先级，分为 Low, Normal, High，通常用 Normal即可
            //    mailMessage.Priority = MailPriority.Normal;

            //    //3. 设置发件人邮箱地址
            //    mailMessage.From = new MailAddress(FromMial);

            //    //4. 设置收件人邮箱地址。需要群发就写多个
            //    //拆分邮箱地址  
            //    List<string> ToMiallist = ToMial.Split(';').ToList();
            //    for (int i = 0; i < ToMiallist.Count; i++)
            //    {
            //        mailMessage.To.Add(new MailAddress(ToMiallist[i]));  //收件人邮箱地址。
            //    }

            //    if (ReplyTo == "" || ReplyTo == null)
            //    {
            //        ReplyTo = FromMial;
            //    }

            //    //对方回复邮件时默认的接收地址(不设置也是可以的哟)
            //    //mailMessage.ReplyTo = new MailAddress(ReplyTo);

            //    //5. 设置抄送人
            //    if (CCMial != "" && CCMial != null)
            //    {
            //        List<string> CCMiallist = ToMial.Split(';').ToList();
            //        for (int i = 0; i < CCMiallist.Count; i++)
            //        {
            //            //邮件的抄送者，支持群发
            //            mailMessage.CC.Add(new MailAddress(CCMial));
            //        }
            //    }
            //    //6. 设置邮件编码 否则乱码
            //    //如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。
            //    mailMessage.SubjectEncoding = Encoding.GetEncoding(936);

            //    //7. 设置邮件正文是否是HTML格式
            //    mailMessage.IsBodyHtml = true;

            //    //8. 设置邮件标题。
            //    mailMessage.Subject = Subject;// "发送邮件测试";
            //    //9. 设置邮件内容。
            //    mailMessage.Body = Body;// "测试群发邮件,以及附件邮件！.....";

            //    //10. 设置邮件的附件，将在客户端选择的附件先上传到服务器保存一个，然后加入到mail中  
            //    if (File_Path != "" && File_Path != null)
            //    {
            //        //将附件添加到邮件
            //        mailMessage.Attachments.Add(new Attachment(File_Path));
            //        //获取或设置此电子邮件的发送通知。
            //        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            //    }

            //    //11. 实例化一个SmtpClient类。
            //    SmtpClient client = new SmtpClient();

            //    #region 设置邮件服务器地址

            //    //如果使用的是qq邮箱，那么就是smtp.qq.com。
            //    // client.Host = "smtp.163.com";
            //    if (FromMial.Length != 0)
            //    {
            //        //根据发件人的邮件地址判断发件服务器地址   默认端口一般是25
            //        string[] addressor = FromMial.Trim().Split(new Char[] { '@', '.' });
            //        switch (addressor[1])
            //        {
            //            case "163":
            //                client.Host = "smtp.163.com";
            //                break;
            //            case "126":
            //                client.Host = "smtp.126.com";
            //                break;
            //            case "qq":
            //                client.Host = "smtp.qq.com";
            //                client.Port = 587;
            //                break;
            //            case "gmail":
            //                client.Host = "smtp.gmail.com";
            //                break;
            //            case "hotmail":
            //                client.Host = "smtp.live.com";//outlook邮箱
            //                //client.Port = 587;
            //                break;
            //            case "foxmail":
            //                client.Host = "smtp.foxmail.com";
            //                break;
            //            case "sina":
            //                client.Host = "smtp.sina.com.cn";
            //                break;
            //            default:
            //                client.Host = "smtp.exmail.qq.com";//qq企业邮箱
            //                client.Port = 587;
            //                break;
            //        }
            //    }
            //    #endregion

            //    //使用安全加密连接。
            //    client.EnableSsl = true;
            //    //不和请求一块发送。
            //    client.UseDefaultCredentials = false;

            //    //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            //    client.Credentials = new NetworkCredential(FromMial, AuthorizationCode);

            //    //如果发送失败，SMTP 服务器将发送 失败邮件告诉我  
            //    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //    //发送
            //    client.Send(mailMessage);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }
#endregion
    }
}