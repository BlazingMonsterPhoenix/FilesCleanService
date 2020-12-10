using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CleanJobService
{
    /**
     * 配置文件检查
     * */
    class ConfigChecker
    {
        
        public static void check()
        {
            //若不存在配置文件则创建
            createIfIsNotExist();
            //配置文件为空则初始化
            if (needToInit())
            {
                intiConfig();
            }
        }

        /**
         * 检查配置文件夹和配置文件是否存在
         * 若不存在则创建
         * */
        private static void createIfIsNotExist()
        {
            string configDirectory = GlobleParams.getConfigDirectory();
            string filePath = GlobleParams.getConfigPath();
            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }
        }

        /**
         * 检查配置文件是否需要初始化
         * 配置文件为空则认为需要初始化
         * */
        private static bool needToInit()
        {
            string filePath = GlobleParams.getConfigPath();
            if (File.ReadAllLines(filePath, Encoding.GetEncoding("gb2312")).Length == 0)
            {
                return true;
            }
            return false;
        }

        /**
         * 初始化配置文件
         * */
        private static void intiConfig()
        {
            string filePath = GlobleParams.getConfigPath();
            string initialConfigString = getInitialConfigString();
            writeConfig(filePath, initialConfigString);
        }


        /**
         * 向配置文件中写内容
         * */
        private static void writeConfig(string configPath, string configString)
        {
            //FileStream fs = new FileStream(configPath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(configPath, true, Encoding.GetEncoding("gb2312"));
            sw.WriteLine(configString);
            sw.Dispose();
            //fs.Dispose();
        }

        /**
         * 获取配置文件的初始内容
         * */
        private static string getInitialConfigString()
        {
            StringBuilder sb = new StringBuilder();
            //执行频率（单位：分钟）
            sb.AppendLine("TF:30"); 
            //删除百分比（单位：%）
            sb.AppendLine("DP:10");
            sb.AppendLine("FP1:");
            sb.AppendLine("SL1:");
            return sb.ToString();
        }

    }
}
