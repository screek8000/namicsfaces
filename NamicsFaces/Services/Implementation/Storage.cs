using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NamicsFaces.Services.Implementation
{
    public class Storage : IStorage
    {
        public Stream RetrieveBlob(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid StoreBlob(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}