using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.RBAC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EF.RBAC.Controllers.Tests
{
    [TestClass()]
    public class Sys_NavButtonsControllerTests
    {
        [TestMethod()]
        public void NavButtonsTest()
        {
            var a= new[] {1,2,3};
            var b=new []{2,3,4,5,6};

            var c = (from i in a select i).Union(
                from i in b select i
                );

            EF.RBAC.Controllers.Sys_NavButtonsController aa = new Sys_NavButtonsController();
            aa.NavButtons(2);
        }
    }
}
