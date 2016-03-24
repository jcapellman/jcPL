using System;
using System.Web.Mvc;

using jcPL.ASPNET.Lib;
using jcPL.PCL.Implementations;

namespace jcPL.UnitTests.MVC.Controllers {
    public class HomeController : Controller {        
        public ActionResult Index() {
            var jsonPL = new JSONPL(new ASPNETPS());

            var result = jsonPL.Get<string>("Testing");

            var finalResult = string.Empty;

            if (!result.HasValue) {
                finalResult = DateTime.Now.ToString();

                jsonPL.Put("Testing", finalResult);
            } else {
                finalResult = result.Value;
            }

            return View("Index", model: finalResult);
        }
    }
}