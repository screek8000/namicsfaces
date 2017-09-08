using NamicsFaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NamicsFaces.Services
{
    public interface IFacesTrainApi
    {
        void AddFace();

        void TrainFaces();

        Task<IEnumerable<PersonMetaData>> GetPersonsMetaDataAsync();
    }
}
