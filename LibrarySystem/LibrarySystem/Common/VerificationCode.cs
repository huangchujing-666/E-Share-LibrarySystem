using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Common
{
    public static class VerificationCode
    {
        //产生验证码的字符集
        private static string[] ValidateCharArray = new string[] { "2", "3", "4", "5", "6", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "M", "N", "P", "R", "S", "U", "W", "X", "Y", "a", "b", "c", "d", "e", "f", "g", "h", "j", "k", "m", "n", "p", "r", "s", "u", "w", "x", "y" };
        //产生验证码的算术符号
        private static string[] ArithmeticSymbol = new string[] { "＋", "－", "×", "÷" };
        //64位图片字符串开头描述
        private const string imageStr = "data:image/{0};base64,";

        ///<summary>
        ///生成随机验证码（数字字母）
        ///</summary>
        ///<param name="length">验证码的长度</param>
        ///<returns></returns>
        public static string CreateValidateNumber(int length)
        {
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(ValidateCharArray.Length);
                if (temp != -1 && temp == t)
                {
                    return CreateValidateNumber(length);
                }
                temp = t;
                randomCode += ValidateCharArray[t];
            }
            return randomCode;
        }
    }
}