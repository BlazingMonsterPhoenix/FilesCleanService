using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CleanJobService
{
    /**
     * 清理文件递送类
     * 将需要删除的文件的列表递送出去
     * */
    class CleanFileDeliver
    {
        public static FileInfo[] deliveList(string path, int percent)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] fileList = getDeleteList(directory.GetFiles(), percent);
            return fileList;
        }

        private static FileInfo[] getDeleteList(FileInfo[] fileList, int percent)
        {
            int deleteNum = fileList.Length * percent / 100;
            return FilesSortor.getLastFilesByCreateTime(fileList,deleteNum);
        }

    }
}
