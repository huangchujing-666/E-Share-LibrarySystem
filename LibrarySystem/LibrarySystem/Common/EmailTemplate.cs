using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Common
{
    public static class EmailTemplate
    {
        //模板类型
        public enum EmailTemplete {
            E建漂流图书服务系统注册验证码 = 1,
            E建漂流图书服务系统登录验证码 = 2,
            E建漂流图书服务系统借书服务 = 3,
            E建漂流图书服务系统续借服务 = 4,
            E建漂流图书服务系统还书服务 = 5,
            E建漂流图书服务系统图书召回服务 = 6,
            E建漂流图书服务系统图书漂入服务 = 7,
            E建漂流图书服务系统图书漂出服务 = 8,
            E建漂流图书服务系统图书求书服务 = 9,
            E建漂流图书服务系统图书送书服务 = 10,
            E建漂流图书服务系统重置密码发送验证码 = 11,
            E建漂流图书服务系统图书寄送服务=12,
            E建漂流图书服务系统图书催还服务 = 13,
            E建漂流图书服务系统 = 14
        }
        /// <summary>
        /// 模板
        /// </summary>
        public static Dictionary<int, string> EmailDictionary = new Dictionary<int, string>()
        {
            {1,"欢迎注册E建漂流图书服务系统，您的注册验证码是：" },
            {2,"欢迎登录E建漂流图书服务系统，您的登录验证码是：" },
            {3,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;您已在图书服务系统中顺利借到图书《#Title#》，请您于#BackTime#前归还此书，否则将按照逾期处理。</div><div>&nbsp; &nbsp; &nbsp;祝您看书愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {4,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;您已在图书服务系统中顺利续借图书《#Title#》，请您于#BackTime#前归还此书，否则将按照逾期处理。</div><div>&nbsp; &nbsp; &nbsp;祝您看书愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {5,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;您已归还图书《#Title#》，感谢您的使用。</div><div>&nbsp; &nbsp; &nbsp;祝您生活愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {6,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;您所借的图书《#Title#》归还日期为：#BackTime#，请在归还期前及时归还，避免造成逾期。</div><div>&nbsp; &nbsp; &nbsp;祝您生活愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {7,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;恭喜您顺利在E建漂流图书服务系统漂入图书《#Title#》,</div><div>&nbsp; &nbsp; &nbsp;祝您看书愉快。</div><div><br></div><div style=;text-align: right;'>【E建漂流图书服务系统】</div>" },
            {8,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;感谢您在E建漂流图书服务系统漂出#Count#本图书《#Title#》,感谢您的付出，祝您生活愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {9,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;恭喜您在E建漂流图书服务系统顺利求得图书《#Title#》,祝您看书愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {10,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;感谢您在E建漂流图书服务系统帮助其他漂书友顺利送书，祝您生活愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {11,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;您正在重置密码，您的验证码是#Code#。</div><div>&nbsp; &nbsp; &nbsp;祝您生活愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            {12,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;您的图书《#Title#》已经寄出，快递信息为：<br>&nbsp; &nbsp; &nbsp;快递名：#ExpressName#；快递编号：#ExpressNo#；费用：#TrafficFee#元。</div><div>&nbsp; &nbsp; &nbsp;祝您生活愉快。</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>" },
            { 13,"<div>尊敬的用户，</div><div>&nbsp; &nbsp; &nbsp;您在E建漂流图书服务系统借阅的图书#Title#已经超期，请您及早归还此书并缴纳相关费用。</div><div>&nbsp; &nbsp; &nbsp;祝您生活愉快！</div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div>"},
            { 14,"<div>亲爱的 #Account# 用户，</div><div><br></div><div>&nbsp; &nbsp; &nbsp;&nbsp;您好！欢迎您注册成为E建漂流图书服务系统会员！<img src='https://rescdn.qqmail.com/zh_CN/images/mo/DEFAULT2/63.gif'></div><div><br></div><div>&nbsp; &nbsp; &nbsp; 在这里，您可以免费获取到E建漂流平台上的共享闲置图书，帮助您节省购书的开支；</div><div><br></div><div>&nbsp; &nbsp; &nbsp; 在这里，您可以低价求购您所需的图书，让您可以与出书人一同品味图书的精彩绝伦；</div><div><br></div><div>&nbsp; &nbsp; &nbsp; 在这里，您可以将您闲置的教材图书漂流至平台，我们将会把它漂流有需要的人手中；</div><div><br></div><div>&nbsp; &nbsp; &nbsp; 在这里，您可以顺路为漂友送书，不仅帮助了别人，还能获得一定的酬劳；</div><div><br></div><div>&nbsp; &nbsp; &nbsp; 在这里，您可以足不出校免费借阅本校、外校的所有图书，解决您无法在本校借阅到所需图书的烦恼，享受跨校图书借还、续借一站式五星服务。</div><div><br></div><div>&nbsp; &nbsp; &nbsp; “全心全意为师生服务”是E建漂流图书服务系统的宗旨。</div><div><br></div><div>&nbsp; &nbsp; &nbsp; 我们致力于为广大师生提供流畅、良好用户体验度地服务系统；</div><div><br></div><div>&nbsp; &nbsp; &nbsp; 我们将招募勤工俭学学生作为漂流书屋的工作人员，作为平台的主人享用平台，并为他们提供获取生活费用的来源；</div><div><br></div><div>&nbsp; &nbsp; &nbsp; 我们将促进校与校之间的图书资源共享，构建图书资源共同体，开创E建漂流平台的美好明天！</div><div><br></div><div><br></div><div style='text-align: right;'>【E建漂流图书服务系统】</div><div>&nbsp; &nbsp; &nbsp; </div><div>&nbsp; &nbsp; &nbsp;</div><div>&nbsp; &nbsp; &nbsp; </div>"}
        };
    }
}