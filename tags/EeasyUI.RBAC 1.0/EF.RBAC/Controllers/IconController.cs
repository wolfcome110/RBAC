using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF.RBAC.Controllers
{
    public class IconController : Controller
    {
        //
        // GET: /Icon/
        public ActionResult Icon16()
        {
            return View();
        }
        public ActionResult Icon32()
        {
            return View();
        }
        
        /// <summary>
        /// 暂时无用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetIcons()
        {
            try
            {
                var dirc = "/Content/icons-customed/icon/16/";
                IEnumerable<string> files = Common.FileHelper.GetFileNames(Server.MapPath(dirc));
                var icons = from f in files select new { name = f.Substring(f.LastIndexOf('\\') + 1).Split('.')[0], path = dirc + f.Substring(f.LastIndexOf('\\') + 1) };
                return Json(new { success = true, Message = "ok！", Icons = icons.ToArray() }, "text/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = "保存失败！" }, "text/json", JsonRequestBehavior.AllowGet);
            }

        }
    }
}