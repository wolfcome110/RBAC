using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Common
{
    public class PostData
    {
        /// <summary>
        /// 方法ID,即config.xml文件对的Function节点的id值
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 提交的数据
        /// </summary>
        public string Parameter { get; set; }
    }
}
