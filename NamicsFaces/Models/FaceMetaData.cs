using System;
using Microsoft.ProjectOxford.Face.Contract;
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

        public FacialHair FacialHair { get; set; }

        public string FacialHairFormatted
        {
            get
            {
                if (FacialHair.Beard == 0 && FacialHair.Moustache == 0 && FacialHair.Sideburns == 0) return "no";

                return "yes (Beard " + NumberHelpers.ToPercent(FacialHair.Beard) + ", Moustache " + NumberHelpers.ToPercent(FacialHair.Moustache) + ", Sideburns " + NumberHelpers.ToPercent(FacialHair.Sideburns) + ")";
            }
        }
    }
}