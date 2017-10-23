using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.RBAC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Data.Entity;

namespace EF.RBAC.Models.Tests
{
    [TestClass()]
    public class UsersTests
    {
        private static RBACEntities db = new RBACEntities();
        [TestMethod()]
        public void UserAuthorityTest()
        {
            
            var rows = db.Set<Sys_Users>()
                        .Include(o => o.Sys_Users_Departments)
                        .Include(o => o.Sys_Users_Groups)
                        .Include(o => o.Sys_Users_Roles)
                        .Include(o => o.Sys_UserNavBtns)
                        .Where(o => o.Id==1);
            Sys_Users user = rows.FirstOrDefault();
            Sys_Navigations nav=db.Sys_Navigations.Where(o=>o.Id==2).FirstOrDefault();
            //Users.UserAuthority(user, nav);

        }

        [TestMethod()]
        public void UserDesktopTest()
        {
            string aa = Users.UserDesktop();
        }
    }
}
