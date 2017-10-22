using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using EF.Common;
using System.IO;
using EF.RBAC.Models;
using System.Web.Security;

namespace EF.RBAC.Controllers
{
    public class DataBaseController : BaseController
    {
        private RBACEntities db = new RBACEntities();
        private string dirc = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Bak/");

        // GET: /DataBase/
        [AuthenticateAttribute]
        public ActionResult Index(int NavId = 0)
        {
            string cookie = CookieHelper.GetCookie(FormsAuthentication.FormsCookieName);

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie);
            Sys_Users user = JsonHelper.DeserializeData<Sys_Users>(ticket.UserData);
            //user=
            //var routeData = string.Format("{0}/{1}", RouteData.Values["controller"], RouteData.Values["action"]);// ;
            var authority = Users.UserAuthority(user, NavId);
            ViewBag.UserAuthority = authority;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int? page, int? rows)
        {
            page = page == null ? 1 : page;
            rows = rows == null ? 20 : rows;

            //string dirc = Server.MapPath("/App_Data/Bak");
            string[] fileNames = FileHelper.GetFileNames(dirc);
            List<object> files = new List<object>();
            foreach (string file in fileNames)
            {
                FileInfo f = new FileInfo(file);
                files.Add(new { FileName = f.Name, CreateTime = f.CreationTime });
            }
            return Json(new
            {
                total = files.Count(),
                rows = files.ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatabaseList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DatabaseList(int? page, int? rows)
        {
            try
            {
                var database = db.Database.SqlQuery<string>("select name from sys.databases where name in ('RBAC')").ToArray();
                var tables = db.Database.SqlQuery<string>("select name from sysobjects where xtype='u'").ToArray();
                List<object> databaseList = new List<object>();
                List<object> tablesList = new List<object>();

                for (int i = 0; i < database.Count(); i++)
                {
                    List<object> listChildren = new List<object>();
                    for (int ii = 0; ii < tables.Count(); ii++)
                    {
                        listChildren.Add(new { id = "tb" + string.Format("{0:D3}", ii), text = tables[ii], iconCls = "iconClsTable", attributes = "" });
                    }
                    databaseList.Add(new { id = "db" + string.Format("{0:D3}", i), text = database[i], iconCls = "iconClsDatabase", attributes = "", children = listChildren.ToArray() });
                }

                return Json(databaseList.ToArray(), "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Backup(MyDatabase model)
        {
            try
            {
                model.DatabaseName = "RBAC";
                //string dirc = Server.MapPath("/App_Data/Bak/");
                string fileName = model.FileName;
                if (!FileHelper.IsExistDirectory(dirc))
                {
                    FileHelper.CreateDirectory(dirc);
                }
                MvcHelper.BackupDataBase(model.DatabaseName, dirc + fileName);
                return Json(new { success = true, Message = "保存成功！", data = model }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Restore(MyDatabase model)
        {
            try
            {
                model.DatabaseName = "RBAC";
                //string dirc = Server.MapPath("/App_Data/Bak/");
                MvcHelper.RestoreDateBase(model.DatabaseName, dirc + model.FileName);
                return Json(new { success = true, Message = "保存成功！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Delete(string Rows)
        {
            try
            {
                List<MyDatabase> databases = JsonHelper.DeserializeData<List<MyDatabase>>(Rows);
                foreach (var file in databases)
                {
                    FileHelper.DeleteFile(dirc + file.FileName);
                }
                //string dirc = Server.MapPath("/App_Data/Bak/");

                return Json(new { success = true, Message = "保存成功！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
