using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Tankerz.Helper
{
    public static class StringHelper
    {
        public static string ConvertUploadFileName(string text)
        {
            for (int index = 33; index < 48; ++index)
                text = text.Replace(((char)index).ToString(), "");
            for (int index = 58; index < 65; ++index)
                text = text.Replace(((char)index).ToString(), "");
            for (int index = 91; index < 97; ++index)
                text = text.Replace(((char)index).ToString(), "");
            for (int index = 123; index < (int)sbyte.MaxValue; ++index)
                text = text.Replace(((char)index).ToString(), "");
            text = text.Replace(" ", "-");
            string str = new Regex("[^0-9a-zA-Z-]+").Replace(new Regex("\\p{IsCombiningDiacriticalMarks}+").Replace(text.Normalize(NormalizationForm.FormD), string.Empty).Replace('đ', 'd').Replace('Đ', 'D'), string.Empty);
            while (str.Contains("--"))
                str = str.Replace("--", "-");
            return (str.StartsWith("-") ? str.Remove(0, 1) : (str.EndsWith("-") ? str.Remove(str.Length - 1) : str)).ToLower();
        }
        public static string GenerateSlug(this string phrase)
        {
            phrase = new Regex("[^0-9a-zA-Z-]+").Replace(new Regex("\\p{IsCombiningDiacriticalMarks}+").Replace(phrase.Normalize(NormalizationForm.FormD), string.Empty).Replace('đ', 'd').Replace('Đ', 'D'), " ");

            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();

            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }
        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
