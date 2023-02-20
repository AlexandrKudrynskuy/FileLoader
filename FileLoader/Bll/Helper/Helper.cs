using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileLoader.Helper
{
    public static class Helper
    {
        public static string FileName(this string path)
        {
            string result = string.Empty;
            string patern = @"[^\/]+$";
            var regex = new Regex(patern);
            var res = regex.Matches(path);
            if (res.Count>0)
            {
                foreach (Match match in res)
                {
                    result = match.Groups[0].Value;
                }
            }
            return result;
        }
    }
}
