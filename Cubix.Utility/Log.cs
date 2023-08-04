using Cubix.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cubix.Utility
{
   public class Log
    {
        public static void LogError(Exception ex, int z = 0)
        {
            string FileName = DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string RenameFileName = DateTime.Now.ToString("yyyyMMdd") + "_" + Guid.NewGuid().ToString().Substring(0, 8) + ".txt";
            string FullFileName = Path.Combine(AppSetting.ErrorLog, FileName);
            if (true)
            {
                if (File.Exists(FullFileName))
                {
                    FileInfo Info = new FileInfo(FullFileName);
                    if (Info.Length > (1024 * 1024 * 2)) // 2 MB Size Only Allowd
                    {
                        string ReFullFileName = Path.Combine(AppSetting.ErrorLog, RenameFileName);
                        File.Copy(FullFileName, ReFullFileName);
                        Info = null;
                        File.Delete(FullFileName);

                    }
                }


                string Content = string.Format("{0}\r\n\r{1}\r\n\r\n{2}\r\n\r{3}\r\n\r{4}\r\n{5}\r\n{6}\r\n\rn{7}\r\n\r", "", "ERROR OCCURRED", "DATE & TIME: " + DateTime.Now.ToString("MM-dd-yyyy") + " " + DateTime.Now.ToLongTimeString(), "SOURCE: " + ex.Source, "METHOD: " + ex.TargetSite, "ERROR: " + ex.InnerException, "STACKTRACE: " + ex.StackTrace, "MESSAGE: " + ex.Message);
                File.AppendAllText(FullFileName, Content);
            }
        }
    }
}
