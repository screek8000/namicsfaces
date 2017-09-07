using NamicsFaces.Models;

namespace NamicsFaces.Services
{
    public interface IFacesApi
    {
        FaceMetaData GetMetaData(string pictureUrl);
    }
}
