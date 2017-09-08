using NamicsFaces.Services;
using NamicsFaces.Services.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NamicsFaces
{
    public class ImageController : ApiController
    {
        // GET api/<controller>/5
        public Stream Get(Guid imageId)
        {
            // TODO: get from blob storage and return binary
            IStorage storage = new Storage();
            return storage.RetrieveBlob(imageId);
        }

    }
}