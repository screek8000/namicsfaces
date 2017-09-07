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
            return View(new FaceMetaData());
        }

        [HttpPost]
        public ActionResult Index(string pictureUrl)
        {
            IFacesApi facesApi = new FacesApi();
            return View(facesApi.GetMetaData(pictureUrl));
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