using EF.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF.RBAC.Controllers
{
    public class DesktopController : BaseController
    {
        //
        // GET: /Desktop/
        public ActionResult Index()
        {
            ViewBag.Title = "管理中心";
            return View();
        }

        
        public string Desktop()
        {
            return Users.UserDesktop();
        }
	}
}