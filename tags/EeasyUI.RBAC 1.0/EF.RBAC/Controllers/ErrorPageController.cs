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
    public class ErrorPageController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /ErrorPage/
        public ActionResult Index()
        {
            return View(db.Sys_Users.ToList());
        }

        public ActionResult Error_404()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
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
