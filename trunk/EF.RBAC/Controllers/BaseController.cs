using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF.RBAC.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string a = "aaa";
        }

        public string demo()
        {
            string a = "11";

            for (int i = 0; i < 100; i++)
            {

                string conn = "server=10.0.0.5;database=bpm;uid=sa;pwd=ice";
                DataSet ds = DBUtility.SqlHelper.Query(conn, "SELECT *  FROM [bpm].[dbo].[Demo_Users]", System.Data.CommandType.Text, null);


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    a += row[2].ToString();
                }

                DataSet d = DBUtility.SqlHelper.Query(conn, "select * from Sys_Buttons", System.Data.CommandType.Text, null);
                foreach (DataRow row in d.Tables[0].Rows)
                {
                    a += row[1].ToString();
                }

                DataSet dd = DBUtility.SqlHelper.Query(conn, "select * from Sys_Departments", System.Data.CommandType.Text, null);
                foreach (DataRow row in dd.Tables[0].Rows)
                {
                    a += row[1].ToString();
                }

            }
            //DBUtility.SqlHelper.ExecuteNonQuery(conn, "update [Demo_Users] set name='bb'", null);
            return a;
        }
    }
}