using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVC_UnitTestSample.Controllers;
using MVC_UnitTestSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_UnitTestSample.Controllers.Tests
{
    [TestClass()]
    public class NameControllerMoqTests
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
            var mock = new Mock<NameRepository>(); //step1. 建立Mock物件
            mock.Setup(p => p.GetNameByData("Tom")).Verifiable();//step2. 設定(setup)模擬此物件時，要執行mothod的方法，而且要被執行過才算數(varifiable)//actual

            var Result = controller.Index(mock.Object.ToString()) as ViewResult;//step3. 設定完成後取出被 mock 過的物件


            //assert
            Assert.AreEqual(Result.ViewBag.Name, "Tom");
        }
    }
}