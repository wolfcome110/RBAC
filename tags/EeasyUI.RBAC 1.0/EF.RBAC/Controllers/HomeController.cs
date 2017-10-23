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
    [RoutePrefix("admin")]
    public class HomeController : BaseController
    {
        private RBACEntities db = new RBACEntities();

        // GET: /Home/
        //[Route("admin/index")]
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Route]
        [Route("index")]
        public ActionResult MainForm()
        {
            return View();
        }

        public ActionResult Desktop()
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



