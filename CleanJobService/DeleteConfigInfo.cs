using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanJobService
{
    /**
     * 删除配置信息
     * */
    class DeleteConfigInfo
    {
        //路径
        private string path;
        //删除界线（超过多少文件时进行删除）
        private int deleteLimit;

        public void setPath(string p)
        {
            this.path = p;
        }

        public void setDeleteLimit(string dl)
        {
            this.deleteLimit = int.Parse(dl);
        }


        public string getPath()
        {
            return this.path;
        }

        public int getDeleteLimit()
        {
            return this.deleteLimit;
        }

    }
}
