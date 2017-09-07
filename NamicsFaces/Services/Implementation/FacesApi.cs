using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NamicsFaces.Models;

namespace NamicsFaces.Services.Implementation
{
    public class FacesApi : IFacesApi
    {
        public FaceMetaData GetMetaData(string pictureUrl)
        {
            return new FaceMetaData()
            {
                ImageUrl = pictureUrl,
                Age = 30,
                Gender = "male"
            };
        }
    }
}