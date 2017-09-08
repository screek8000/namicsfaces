using System;
using NamicsFaces.Helpers;

namespace NamicsFaces.Models
{
    public class FaceMetaData
    {
        public string ImageUrl { get; set; }

        public double Age { get; set; }

        public string Gender { get; set;}

        public double Smile { get; set; }

        public string SmilePercent
        {
            get
            {
                return NumberHelpers.ToPercent(Smile);
            }
        }

        public string Glasses { get; set; }

        public string Emotion { get; set; }
        public Func<string> FacialHair { get; set; }
    }
}