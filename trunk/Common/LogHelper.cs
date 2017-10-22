﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LogHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void Debug(object ex)
        {
            Log.Debug(ex);
        }

        public static void Warn(object ex)
        {
            Log.Warn(ex);
        }

        public static void Error(object ex)
        {
            Log.Error(ex);
        }

        public static void Info(object ex)
        {
            Log.Info(ex);
        }

        public static void Debug(object message, Exception ex)
        {
            Log.Debug(message, ex);
        }

        public static void Warn(object message, Exception ex)
        {
            Log.Warn(message, ex);
        }

        public static void Error(object message, Exception ex)
        {
            Log.Error(message, ex);
        }

        public static void Info(object message, Exception ex)
        {
            Log.Info(message, ex);
        }
    }
}
