using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using ZacamoMvc.ManufacturerServiceReference;
using ZacamoMvc.Models;
using ZacamoMvc.ProductServiceReference;

namespace ZacamoMvc.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchProductViewModel model)
        {
            try
            {
                //return View("../Product/ShowProducts", new {searchTerm = model.SearchTerm});
                return RedirectToAction("Index", "Product", new {searchTerm = model.SearchTerm});
            }
            catch
            {
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}