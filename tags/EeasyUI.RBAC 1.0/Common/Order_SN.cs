using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Common
{
    public class Order_SN
    {
        private static object locker = new object();

        private static int sn = 0;

        public static string NextOrderSN()
        {
            lock (locker)
            {
                if (sn == 9999)
                    sn = 0;
                else
                    sn++;
                return DateTime.Now.ToString("yyyyMMddHHmmss") + sn.ToString().PadLeft(4, '0');
            }
        }
        // 防止创建类的实例
        private Order_SN() { }
    }
}
