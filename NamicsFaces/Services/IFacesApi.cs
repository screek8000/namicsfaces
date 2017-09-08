using System.Threading.Tasks;
using System.Web;
using Microsoft.ProjectOxford.Face.Contract;
using NamicsFaces.Models;

namespace NamicsFaces.Services
{
    public interface IFacesApi
    {
        FaceMetaData GetMetaData(string pictureUrl);

        Task<PersonMetaData> Identify(string pictureUrl);

        Task<PersonMetaData> Identify(HttpPostedFileBase file);
    }
}
