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
        INameRepository _NameRepository;

        public NameController()
        {
            _NameRepository = new NameRepository();
        }

        public NameController(INameRepository nameRepository)
        {
            _NameRepository = nameRepository;
        }

        // GET: Name
        public ActionResult Index(string name)
        {
            ViewBag.Name = _NameRepository.GetNameByData(name);
            return View();
        }
    }
}