using NamicsFaces.Models;
using NamicsFaces.Services;
using NamicsFaces.Services.Implementation;
using System.Threading.Tasks;
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
		public async Task<ActionResult> Identify(string pictureUrl)
	    {
		    IFacesApi facesApi=new FacesApi();
		    PersonMetaData result = await facesApi.Identify(pictureUrl);

		    return View("Identify", result);
	    }
	   
        public async Task<ActionResult> Train()
        {
            IFacesTrainApi facesTrainApi = new FacesTrainApi();
            var trainModel = new TrainModel()
            {
                Mode = "Get",
                Persons = await facesTrainApi.GetPersonsMetaDataAsync()
            };
            return View("Train", trainModel);
        }
        
        [HttpPost]
        public ActionResult Train(HttpPostedFileBase file, string personId)
        {
            var trainModel = new TrainModel()
            {
                Mode = "File"                
            };
            if (file != null)
            {
                trainModel.Result = "OK";
                return View("Train", trainModel);
            } else
            {
                trainModel.Result = "NOK";
                return View("Train", trainModel);
            }
        }

        [HttpPost]
        public ActionResult StartTrain()
        {
            IFacesTrainApi facesTrainApi = new FacesTrainApi();
            facesTrainApi.TrainFaces();
            return View("Trainstatus");
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