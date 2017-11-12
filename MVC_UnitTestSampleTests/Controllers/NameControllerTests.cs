using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_UnitTestSample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_UnitTestSample.Controllers.Tests
{
   
    [TestClass()]
    public class NameControllerTests
    {

        private static NameController GetNameController()
        {
            NameController controller = new NameController();
            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext= new  RequestContext(new )
            }
            return controller;
        }

        [TestMethod()]
        public void IndexTest()
        {
            var controller = GetNameController();
            controller.Index();
            Assert.Fail();
        }
    }
}