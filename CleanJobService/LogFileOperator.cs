using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CleanJobService
{
    class LogFileOperator
    {
        /**
         * 检查日志文件及文件夹是否存在，不存在则创建
         * */
        public static void check()
        {
            string logDirectory = GlobleParams.getLogDirectory();
            string filePath = GlobleParams.getLogPath();
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }
        }

        /**
         * 写日志
         * */
        public static void writeLog(string log)
        {
            StreamWriter sw = new StreamWriter(GlobleParams.getLogPath(), true, Encoding.GetEncoding("gb2312"));
            sw.WriteLine(log);
            sw.Close();
            sw.Dispose();
        }

    }
}
