using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanJobService
{
    class GlobleParams
    {
        //配置文件所在文件夹目录
        private static string configDirectory = "C:\\CleanJob\\ConfigFile";
        //配置文件文件名
        private static string configFileName = "CleanJobConfig.txt";
        //日志文件所在文件夹目录
        private static string logDirectory = "D:\\ATE_DATA\\CleanJobLog";

        private static string logPath;

        //执行频率（单位：分钟）
        private static int frequency = -1;
        //文件删除百分比
        private static int deletePercent = -1;
        //删除配置列表
        private static List<DeleteConfigInfo> configList = new List<DeleteConfigInfo>();


        /********* setter方法列表 **********/
        public static void setFrequency(string f)
        {
            frequency = int.Parse(f);
        }

        public static void setDeletePercent(string dp)
        {
            deletePercent = int.Parse(dp);
        }

        public static void configListAdd(DeleteConfigInfo d)
        {
            configList.Add(d);
        }

        public static void setLogPath(string lp)
        {
            logPath = lp;
        }

        /********** getter方法列表 **********/
        public static string getConfigDirectory()
        {
            return configDirectory;
        }

        public static string getConfigPath()
        {
            return configDirectory + "\\" + configFileName;
        }

        public static string getLogDirectory()
        {
            return logPath != null ? logPath : logDirectory; ;
        }

        public static string getLogPath()
        {
            string date = DateTime.Today.ToString();
            return getLogDirectory() + "\\log" + date.Substring(0, 4) + date.Substring(5, 2) + date.Substring(8, 2) + ".txt";
        }

        /**
         * 获取执行频率（毫秒）
         * */
        public static int getFrequency()
        {
            return frequency * 60000;
        }

        public static int getDeletePercent()
        {
            return deletePercent;
        }

        public static List<DeleteConfigInfo> getConfigList()
        {
            return configList;
        }

    }
}
