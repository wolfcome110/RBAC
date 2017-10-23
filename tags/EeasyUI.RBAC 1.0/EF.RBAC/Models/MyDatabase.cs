using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF.RBAC.Models
{
    public class MyDatabase
    {
        private string _fileName;
        private string _databaseName;
        private DateTime _createTime = DateTime.Now;
        public string FileName
        {
            set
            {
                _fileName = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                {
                    return _createTime.ToString("yyyyMMddHHmmss.bak");
                }
                else
                {
                    return _fileName;
                }

            }
        }
        public string DatabaseName { get; set; }

        public DateTime CreateTime
        {
            set
            {
                _createTime = value;
            }
            get
            {
                return _createTime;
            }
        }
    }
}