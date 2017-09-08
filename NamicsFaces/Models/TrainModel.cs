using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamicsFaces.Models
{
    public class TrainModel
    {
        public string Mode { get; set; }

        public IEnumerable<PersonMetaData> Persons { get; set; }

        public string Result { get; set; }
    }
}