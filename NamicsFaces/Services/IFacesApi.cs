using System.Threading.Tasks;
using System.Web;
using Microsoft.ProjectOxford.Face.Contract;
using NamicsFaces.Models;

namespace NamicsFaces.Services
{
    public interface IFacesApi
    {
        Task<FaceMetaData> GetMetaData(string pictureUrl);

        Task<FaceMetaData> GetMetaData(HttpPostedFileBase file);

        Task<PersonMetaData> Identify(string pictureUrl);

        Task<PersonMetaData> Identify(HttpPostedFileBase file);
    }
}
