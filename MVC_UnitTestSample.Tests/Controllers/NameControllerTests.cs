using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC_UnitTestSample.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_UnitTestSample.Controllers.Tests
{
    [TestClass()]
    public class NameControllerTests
    {

        private static NameController GetNameController()
        {
            NameController controller = new NameController();

            return controller;
        }

        [TestMethod()]
        public void NameControllerTest_應取得名字Tom()
        {
            //arrange
            var controller = GetNameController();

            //actual
            var Result = controller.Index("Tom") as ViewResult;
            
            //assert
            Assert.AreEqual(Result.ViewBag.Name,"Tom");
        }
    }
}