using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;
using NamicsFaces.Models;

namespace NamicsFaces.Services
{
    public interface IFacesApi
    {
        FaceMetaData GetMetaData(string pictureUrl);

        Task<PersonMetaData> Identify(string pictureUrl);
		
	}
}
