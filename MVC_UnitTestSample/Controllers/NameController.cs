using MVC_UnitTestSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_UnitTestSample.Controllers
{
    public class NameController : Controller
    {
        NameRepository _rsp;
        public NameController()
        {
            _rsp = new NameRepository();
        }

        public NameController(NameRepository rsp)
        {
            _rsp = rsp;
        }
        // GET: Name
        public ActionResult Index(string name)
        {
            ViewBag.Name = _rsp.GetNameByData(name);
            return View();
        }
    }
}