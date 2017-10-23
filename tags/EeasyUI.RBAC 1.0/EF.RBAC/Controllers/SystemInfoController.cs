using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF.RBAC.Controllers
{
    public class SystemInfoController : BaseController
    {
        //
        // GET: /SystemInfo/
        public ActionResult Config()
        {
            return View();
        }

        public ActionResult Individuation()
        {
            return View();
        }

        public ActionResult SystemInfo()
        {
            return View();
        }
	}
}