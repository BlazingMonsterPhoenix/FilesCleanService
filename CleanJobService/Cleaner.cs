using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CleanJobService
{
    class Cleaner
    {
        /**
         * 清理多余文件
         * （入口方法）
         * 描述：依次检查需要清理的目录
         *       如果目录中文件过多，则按照要求清理最早创建的一定数量的文件
         *       并返回清理文件的日志信息
         * */
        public static string cleanUpExcessFiles(List<DeleteConfigInfo> deleteList,int percent)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n************************* 清理文件开始，开始时间为：" + DateTime.Now.ToString() + " ********************");
            foreach (DeleteConfigInfo infor in deleteList)
            {
                sb.Append(cleanUpExcessFilesInThePath(infor, percent));
            }
            sb.AppendLine("************************* 清理文件结束，结束时间为：" + DateTime.Now.ToString() + " ********************\n");
            return sb.ToString();
        }

        /**
         * 清理指定目录下的多余文件
         * 描述：检查指定目录下文件是否过多，如果是
         *       则按照要求清理一定数量的最早创建的文件
         *       并返回本目录下清理文件的日志信息
         * */
        private static StringBuilder cleanUpExcessFilesInThePath(DeleteConfigInfo infor, int percent)
        {
            StringBuilder sb = new StringBuilder();
            //路径
            string path = infor.getPath();
            //删除界线
            int limit = infor.getDeleteLimit();
            //计算文件数量
            int count = countFile(path);
            //向日志字符串中加入开始前信息
            addStartInfor(sb, path, count,limit,percent);
            //检查文件是否过多，如果是则进行清理
            if (isOverLimit(count, limit))
            {
                cleanFiles(path, percent);
            }
            //向日志字符串中加入完成信息
            addEndInfor(sb, path, count, percent);
            return sb;
        }

        /**
         * 向日志字符串中加入开始检查文件夹的信息
         * */
        private static void addStartInfor(StringBuilder sb,string path, int count, int limit, int percent)
        {
            sb.AppendLine("*************** 开始检查路径：" + path);
            sb.AppendLine("*************** 开始时间：" + DateTime.Now.ToString() + " ***************");
            sb.AppendLine("目录下共有文件：" + count + "个");
            sb.AppendLine("超过：" + limit + "个文件时，需要进行清理");
            sb.AppendLine("设置需要清理的百分比为：" + percent + "%");
            int cleanNum = isOverLimit(count, limit) ? (count * percent / 100) : 0;
            sb.AppendLine("应清理文件：" + cleanNum + "个");
        }

        /**
         * 向日志字符串中加入检查完文件夹的信息
         * */
        private static void addEndInfor(StringBuilder sb,string path, int count,int percent)
        {
            sb.AppendLine("\n实际清理文件：" + (count - countFile(path)) + "个");
            sb.AppendLine("*************** 结束时间：" + DateTime.Now.ToString() + " ***************\n");
        }

        /**
         * 计算目录下文件数量
         * */
        private static int countFile(string path)
        {
            return Directory.GetFiles(path).Length;
        }

        /**
         * 计算文件是否超出上限
         * */
        private static bool isOverLimit(int num, int deleteLimit)
        {
            return num > deleteLimit;
        }

        /**
         * 清理文件
         * 描述：清理某一路径下的一定比例的最早创建的文件
         * */
        private static void cleanFiles(string path, int percent)
        {
            //获取需要清理的文件列表
            FileInfo[] fileList = CleanFileDeliver.deliveList(path, percent);
            //LogFileOperator.writeLog("******************* 删除的文件列表 *********************");
            foreach (FileInfo fileInfo in fileList)
            {
                //LogFileOperator.writeLog(fileInfo.FullName + "," + fileInfo.CreationTime);
                fileInfo.Delete();
            }
        }

    }
}
