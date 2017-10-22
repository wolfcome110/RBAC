using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IpHelper
    {

        /// <summary>
        /// IP地址转化
        /// </summary>
        /// <param name="ipaddr">整型的IP地址</param>
        /// <returns>字符串的IP地址</returns>
        public static string UintIPToStringIP(uint ipaddr)
        {
            string hexStr = ipaddr.ToString("X8");
            int ip1 = Convert.ToInt32(hexStr.Substring(0, 2), 16);
            int ip2 = Convert.ToInt32(hexStr.Substring(2, 2), 16);
            int ip3 = Convert.ToInt32(hexStr.Substring(4, 2), 16);
            int ip4 = Convert.ToInt32(hexStr.Substring(6, 2), 16);
            return ip4 + "." + ip3 + "." + ip2 + "." + ip1;
        }

        /// <summary>
        /// IP地址转化
        /// </summary>
        /// <param name="ipaddr">字符串的IP地址</param>
        /// <returns>整型的IP地址</returns>
        public static uint StringIPToUintIP(string ipaddr)
        {
            string[] ips = ipaddr.Split('.');
            return Convert.ToUInt32(ips[3]) * 256 * 256 * 256 + Convert.ToUInt32(ips[2]) * 256 * 256 + Convert.ToUInt32(ips[1]) * 256 + Convert.ToUInt32(ips[0]);
        }

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalIPAddress()
        {
            //string resultIP = string.Empty;
            //System.Net.IPAddress[] ips = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;
            //foreach (System.Net.IPAddress ip in ips)
            //{
            //    if (IsCorrentIP(ip.ToString()))
            //    {
            //        resultIP = ip.ToString();
            //        break;
            //    }
            //}
            //return resultIP;

            ///获取本地的IP地址
            List<string> LocalIPAddress = new List<string>();
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    LocalIPAddress.Add(_IPAddress.ToString());
                }
                
            }
            return LocalIPAddress;
        }

        /// <summary>
        /// 验证IP地址是否有效
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsCorrentIP(string ip)
        {
            string pattrn = @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])";
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, pattrn))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
