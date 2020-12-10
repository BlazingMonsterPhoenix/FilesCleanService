using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CleanJobService
{
    class ConfigReader
    {
        public static void read()
        {
            string path = GlobleParams.getConfigPath();
            //读取配置文件初始化数据
            string[] lines = File.ReadAllLines(path);
            int line = analysisPublicData(lines);
            analysisDeleteConfig(lines, line);
            createGlobleParams();
            printGlobleParams();
        }

        /**
         * 解析公用数据（执行频率和删除百分比）
         * */
        private static int analysisPublicData(string[] lines)
        {
            int flag = 0, i = 0;
            for (; i < lines.Length && flag < 2; i ++)
            {
                if (lines[i].Length > 3)
                {
                    switch (lines[i].Substring(0, 3))
                    {
                        case "TF:": GlobleParams.setFrequency(lines[i].Substring(3, lines[i].Length - 3)); flag++; break;
                        case "DP:": GlobleParams.setDeletePercent(lines[i].Substring(3, lines[i].Length - 3)); flag++; break;
                        default: break;
                    }
                }
            }
            return i;
        }


        private static void analysisDeleteConfig(string[] lines, int line)
        {
            int flag = 0, index = 0,i = line;
            DeleteConfigInfo infor = new DeleteConfigInfo();
            for (; i < lines.Length; i++)
            {
                index = lines[i].IndexOf(":") + 1;
                if (lines[i].Length > 4 && index != -1)
                {
                    switch (lines[i].Substring(0, 2))
                    {
                        case "FP": infor.setPath(lines[i].Substring(index, lines[i].Length - index)); flag += 2; break;
                        case "SL": infor.setDeleteLimit(lines[i].Substring(index, lines[i].Length - index)); flag--; break;
                        default: break;
                    }
                }
                if (flag == 1)
                {
                    GlobleParams.configListAdd(infor);
                    flag = 0;
                }
                else if (flag == -1)
                {
                    break;
                }
            }
        }

        private static void createGlobleParams()
        {
            GlobleParams.setFrequency(GlobleParams.getFrequency() == -60000 ? "30" : (GlobleParams.getFrequency() / 60000).ToString());
            GlobleParams.setDeletePercent(GlobleParams.getDeletePercent() == -1 ? "10" : GlobleParams.getDeletePercent().ToString());
        }

        private static void printGlobleParams()
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine("当前时间：" + DateTime.Now.ToString() + " 服务启动...\n");
            log.AppendLine("执行频率：" + GlobleParams.getFrequency() / 60000 + "分钟");
            log.AppendLine("删除比例：" + GlobleParams.getDeletePercent() + "%");
            log.AppendLine("共读取到：" + GlobleParams.getConfigList().Count() + "个需要检查的路径\n");
            LogFileOperator.writeLog(log.ToString());
        }
    }
}
