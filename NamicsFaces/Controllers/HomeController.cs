using NamicsFaces.Models;
using NamicsFaces.Services;
using NamicsFaces.Services.Implementation;
using System;
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
        public async Task<ActionResult> Analyze(string pictureUrl, HttpPostedFileBase file)
        {
            IFacesApi facesApi = new FacesApi();
            FaceMetaData result;
            if (file != null)
            {
                string imageUrl = UploadImage(file);
                result = await facesApi.GetMetaData(file);
                result.ImageUrl = imageUrl;
            }
            else
            {
                result = await facesApi.GetMetaData(pictureUrl);
            }
            return View("Analyze", result);
        }

        public ActionResult Identify()
        {
            return View("Identify", new PersonMetaData());
        }

       
	    [HttpPost]
		public async Task<ActionResult> Identify(string pictureUrl, HttpPostedFileBase file)
	    {
		    IFacesApi facesApi=new FacesApi();
            PersonMetaData result;
            if (file != null)
            {
                string imageUrl = UploadImage(file);
                result = await facesApi.Identify(file);
                result.ImageUrl = imageUrl;
            } else
            {
                result = await facesApi.Identify(pictureUrl);
            }

		    return View("Identify", result);
	    }
	   
        public async Task<ActionResult> Train()
        {
            IFacesTrainApi facesTrainApi = new FacesTrainApi();
            var trainModel = new TrainModel()
            {
                Persons = await facesTrainApi.GetPersonsMetaDataAsync()
            };
            return View("Train", trainModel);
        }
        
        [HttpPost]
        public async Task<ActionResult> Train(HttpPostedFileBase file, string personId, string personName)
        {
            IFacesTrainApi facesTrainApi = new FacesTrainApi();
            
            var trainModel = new TrainModel();
            if (file != null)
            {
                await facesTrainApi.AddFaceAsync(file, personId, personName);
                trainModel.Result = "File uploaded";
            } else
            {
                trainModel.Result = "No file uploaded";
            }
            trainModel.Persons = await facesTrainApi.GetPersonsMetaDataAsync();
            return View("Train", trainModel);
        }

        [HttpPost]
        public ActionResult StartTrain()
        {
            IFacesTrainApi facesTrainApi = new FacesTrainApi();
            facesTrainApi.TrainFaces();
            return RedirectToRoute("/Home/Trainstatus");
        }

        public async Task<ActionResult> Trainstatus()
        {
            IFacesTrainApi facesTrainApi = new FacesTrainApi();
            TrainStatus status = await facesTrainApi.TrainStatusAsync();
            return View("Trainstatus", status);
        }

        private string UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                try
                {
                    string filename = "Uploads/" + Guid.NewGuid() + ".jpg";
                    string physicalPath = Server.MapPath(filename.Replace("/", "\\"));
                    file.SaveAs(physicalPath);
                    return "/Home/" + filename;
                }
                catch (Exception e)
                {
                }
            }
            return "";
        }
    }
}