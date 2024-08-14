using System;

namespace DoctorHelper.Helpers
{
    public class TextHelper
    {
        public static string ReverseText(string s)
        {
            string[] charArray = s.Split(new string[] { " " }, StringSplitOptions.None);
            Array.Reverse(charArray);
            string ReturnedText = string.Empty;
            foreach (var item in charArray)
            {
                ReturnedText += " " + item; 
            }
            return ReturnedText;
        }

        public static string ReverseChar(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
