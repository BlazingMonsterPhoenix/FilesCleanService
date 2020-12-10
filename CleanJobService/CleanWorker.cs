using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace CleanJobService
{
    class CleanWorker
    {
        /**
         * 入口程序
         * */
        public static void work(object sender, ElapsedEventArgs args)
        {
            //检查日志
            LogFileOperator.check();
            //执行文件删除
            string log = Cleaner.cleanUpExcessFiles(GlobleParams.getConfigList(),GlobleParams.getDeletePercent());
            //写日志
            LogFileOperator.writeLog(log);
        }

    }
}
