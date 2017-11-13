using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper.Fakes;
using System.Web.Routing;
using System.Web.Mvc;

namespace MVC_UnitTestSample.Tests.Controllers
{
    [TestClass]
    public class MvcApplicationTests
    {
        [TestMethod]
        public void RegisterRoutesTest()
        {
            //arrange
            RouteCollection routes = new RouteCollection();
            var contextSutb = new FakeHttpContext("~/", "GET");
            //actual
            //MvcApplication.RegisterRoutes(routes); //保哥的文章使用的MVC2
            RouteConfig.RegisterRoutes(routes);
            var routeData = routes.GetRouteData(contextSutb);
            //assert
            Assert.AreEqual("Home",routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);

        }
    }
}
