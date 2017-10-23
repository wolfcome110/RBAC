using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Common
{
    public class JsonHelper
    {
        /// <summary>
        /// 将数据进行Json序列化
        /// </summary>
        /// <param name="data">需要序列化的对象</param>
        /// <returns></returns>
        public static string SerializeData(object data)
        {
            IsoDateTimeConverter dateConvert = new IsoDateTimeConverter();
            dateConvert.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string json = JsonConvert.SerializeObject(data, Formatting.Indented, dateConvert);
            json = JsonConvert.SerializeObject(data, dateConvert);
            return json;
        }

        /// <summary>
        /// 将json字串转换为T对象
        /// </summary>
        /// <typeparam name="T">转换的对象</typeparam>
        /// <param name="jsonString">json字串</param>
        /// <returns></returns>
        public static T DeserializeData<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
