using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CleanJobService
{
    public partial class CleanJobService : ServiceBase
    {
        public CleanJobService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //检查配置文件是否存在，若不存在则创建
            ConfigChecker.check();
            //读取配置文件并初始化全局变量
            ConfigReader.read();
            //检查日志文件
            LogFileOperator.check();
            //在日志中输出启动信息
            printGlobleParams();
            //执行定时清理文件任务
            startTimeJob();
        }

        protected override void OnStop()
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine("\n当前时间：" + DateTime.Now.ToString() + " 服务关闭...\n");
            LogFileOperator.writeLog(log.ToString());
        }

        /**
         * 执行定时任务
         * */
        private void startTimeJob()
        {
            Timer timer = new Timer();
            //执行时间间隔
            timer.Interval = GlobleParams.getFrequency();
            timer.Elapsed += new ElapsedEventHandler(CleanWorker.work);
            timer.Start();
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
