using NamicsFaces.Models;
using NamicsFaces.Services;
using NamicsFaces.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NamicsFaces.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Analyze()
        {
            return View("Analyze", new FaceMetaData());
        }

        [HttpPost]
        public ActionResult Analyze(string pictureUrl)
        {
            IFacesApi facesApi = new FacesApi();
            return View("Analyze", facesApi.GetMetaData(pictureUrl));
        }

        public ActionResult Identify()
        {
            return View("Identify", new PersonMetaData());
        }

        [HttpPost]
        public ActionResult Identify(string pictureUrl)
        {
            IFacesApi facesApi = new FacesApi();
            //return View("Identify", facesApi.Identify(pictureUrl));
	        return View("Identify", facesApi.Identify(pictureUrl));
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