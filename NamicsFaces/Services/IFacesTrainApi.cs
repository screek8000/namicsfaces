using NamicsFaces.Models;
using System.Collections.Generic;

namespace NamicsFaces.Services
{
    public interface IFacesTrainApi
    {
        void AddFace();

        void TrainFaces();

        IEnumerable<PersonMetaData> GetPersons();
    }
}
