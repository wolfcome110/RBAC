using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;

namespace EF.RBAC.Controllers
{
    public class Sys_GroupNavBtnsController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Sys_GroupNavBtns/
        public ActionResult Index()
        {
            var sys_groupnavbtns = db.Sys_GroupNavBtns.Include(s => s.Sys_Buttons).Include(s => s.Sys_Groups).Include(s => s.Sys_Navigations);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save(string Groups, string Navigations, string Buttons)
        {
            try
            {
                List<Sys_Groups> groups = Common.JsonHelper.DeserializeData<List<Sys_Groups>>(Groups);
                List<Sys_Navigations> navigations = Common.JsonHelper.DeserializeData<List<Sys_Navigations>>(Navigations);
                List<Sys_Buttons> buttons = Common.JsonHelper.DeserializeData<List<Sys_Buttons>>(Buttons);

                foreach (var group in groups)
                {
                    foreach (var nav in navigations)
                    {
                        foreach (var button in buttons)
                        {
                            if (db.Set<Sys_GroupNavBtns>().Where(o => o.GroupId == group.Id && o.NavId == nav.Id && o.BtnId == button.Id).Count() == 0)
                            {
                                db.Set<Sys_GroupNavBtns>().Add(new Sys_GroupNavBtns { GroupId = group.Id, NavId = nav.Id, BtnId = button.Id, AddTime = DateTime.Now });
                            }
                        }
                    }
                }

                db.SaveChanges();
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
