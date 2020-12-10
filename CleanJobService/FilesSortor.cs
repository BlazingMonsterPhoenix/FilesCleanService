using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CleanJobService
{
    class FilesSortor
    {
        /**
         * 获取最旧的文件
         * */
        public static FileInfo[] getLastFilesByCreateTime(FileInfo[] fileList, int num)
        {
            //printFilesA(fileList);
            sortByCreateTime(fileList);
            FileInfo[] deleteList = new FileInfo[num];
            for (int i = 0; i < num; i++)
                deleteList[i] = fileList[i];
            //printFiles(fileList);
            return deleteList;
        }

        /*private static void printFilesA(FileInfo[] fileList)
        {
            LogFileOperator.writeLog("******************* 排序前的文件列表 *********************");
            for (int i = 0; i < fileList.Length; i++)
            {
                LogFileOperator.writeLog(fileList[i].FullName + "," + fileList[i].CreationTime);
            }
        }

        private static void printFiles(FileInfo[] fileList)
        {
            LogFileOperator.writeLog("******************* 排序后的文件列表 *********************");
            for (int i = 0; i < fileList.Length; i++)
            {
                LogFileOperator.writeLog(fileList[i].FullName + "," + fileList[i].CreationTime);
            }
        }*/

        /**
         * 将文件列表根据创建时间排序
         * */
        private static FileInfo[] sortByCreateTime(FileInfo[] fileArray)
        {
            shellSortByCreateTime(fileArray, 0, fileArray.Length - 1, fileArray.Length);
            return fileArray;
        }

        /**
         * 根据创建时间对文件进行排序（希尔排序）
         * */
        private static void shellSortByCreateTime(FileInfo[] fileArray, int start, int end, int num)
        {
            int step = (end - start + 1) / num;		//同一组的两个相邻元素的下标差
            if (num > 3)
                shellSortByCreateTime(fileArray, start, end, num / 2);
            for (int i = 0; i < step; i++)			//循环，对每组进行插入排序
                shellInsert(fileArray, start + i, end, step);
        }

        /**
	     * 希尔排序，分组插入排序
	     * @param start 起始元素下标
	     * @param end 最后一个元素下标
	     * @param step 当前组的两个相邻元素的下标差
	     */
        private static void shellInsert(FileInfo[] fileArray, int start, int end, int step)
        {
            for (int j = start + step; j <= end; j += step)			//左边第二个元素插入到第一个元素前或后，然后第三个元素插入到前两个元素组成的数组中，以此类推（从左向右插入）
            {
                FileInfo flag = fileArray[j];			//等待插入的元素		
                for (int i = j - step; i >= start - step; i -= step)
                {
                    //待插入元素比左边所有元素小，或待插入元素的值大于左侧元素（array[i]）的值，该元素放在array[i]右边
                    if (i == start - step || flag.CreationTime.CompareTo(fileArray[i].CreationTime) == 1)
                    {
                        fileArray[i + step] = flag;
                        break;
                    }
                    else		//待插入元素的值比左侧元素小，左侧元素右移
                        fileArray[i + step] = fileArray[i];
                }
            }
        }

    }
}
