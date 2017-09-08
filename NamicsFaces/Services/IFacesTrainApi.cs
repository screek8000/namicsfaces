using NamicsFaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace NamicsFaces.Services
{
    public interface IFacesTrainApi
    {
        void TrainFaces();

        Task<IEnumerable<PersonMetaData>> GetPersonsMetaDataAsync();

        Task AddFaceAsync(HttpPostedFileBase file, string personId, string personName);

        Task<TrainStatus> TrainStatusAsync();
    }
}
