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
        
        private static NameController GetNameController(INameRepository nameRepository)
        {
            NameController controller = new NameController(nameRepository);

            return controller;
        }

        [TestMethod()]
        public void NameControllerTest_應取得名字Tom()
        {
            //arrange
            var mock = new Mock<INameRepository>(); //step1. 建立Mock物件
            mock.Setup(p => p.GetNameByData("Tom")).Returns("Tom");//step2. 設定(setup)模擬此物件時，要執行mothod的方法，回傳Tom
            var controller = GetNameController(mock.Object);

            //actual
            var Result = controller.Index("Tom") as ViewResult;//step3. 設定完成後取出被 mock 過的物件


            //assert
            Assert.AreEqual("Tom", Result.ViewBag.Name);
        }
    }
}