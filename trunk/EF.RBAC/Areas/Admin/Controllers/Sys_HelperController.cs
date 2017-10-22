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
    public class Sys_HelperController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Helper/
        public ActionResult Index()
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
