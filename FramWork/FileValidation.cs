
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork
{
    public static class FileValidation
    {
        public static bool IsValidFileName(this string fileName)
        {
             fileName= Path.GetFileName(fileName).ToLower().Trim();
            if (fileName.Contains(".asp") || fileName.Contains(".py") || fileName.Contains(".jsp") || fileName.Contains(".php") || fileName.Contains(".exe"))
            {
                return false;
            }
            return true;
        }
        public static string ToPersianDateForFileName(this DateTime t)
        {
            PersianCalendar pc  = new PersianCalendar();
            return $"{pc.GetYear(t).ToString()}_{pc.GetMonth(t).ToString()}_{pc.GetDayOfMonth(t).ToString()}_{t.Hour.ToString()}_{t.Minute.ToString()}_{t.Second.ToString()}_{t.Millisecond.ToString()}";
        }
        public static string ToUniqueFileName(this string FileName)
        {
            return $"{Guid.NewGuid().ToString().Replace("-", "")}__{DateTime.Now.ToPersianDateForFileName()}_{System.IO.Path.GetFileName(FileName.ToLower())}";
        }
       
    }
}
