using System;
using System.IO;

namespace NamicsFaces.Services
{
    public interface IStorage
    {
        Guid StoreBlob(Stream stream);

        Stream RetrieveBlob(Guid id);
    }
}
