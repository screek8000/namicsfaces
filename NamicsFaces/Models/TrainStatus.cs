using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ProjectOxford.Face.Contract;

namespace NamicsFaces.Models
{
    public class TrainStatus
    {
        public Status Status { get; internal set; }
        public string Message { get; internal set; }
        public DateTime LastActionDateTime { get; internal set; }
    }
}