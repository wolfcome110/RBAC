using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace EF.Common
{
    /// <summary>
    /// Post参数集合类
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// FCode
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// Post串
        /// </summary>
        public string Where
        {
            set;
            get;
        }
        /// <summary>
        /// 起始页码
        /// </summary>
        public int PageIndex
        {
            set;
            get;
        }
        /// <summary>
        ///  每页总条数
        /// </summary>
        public int PageSize
        {
            set;
            get;
        }

        /// <summary>
        /// 其它定义的参数
        /// </summary>
        public Dictionary<string, string> parameters
        {
            get;
            set;
        }
        /// <summary>
        ///提交的表单数据
        /// </summary>
        public NameValueCollection nameValueCollection { get; set; }
        
    }
}
