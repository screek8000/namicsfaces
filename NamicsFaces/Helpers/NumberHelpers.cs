using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NamicsFaces.Helpers
{
    public static class NumberHelpers
    {
        public static string ToPercent(double value)
        {
            return (int)(value * 100) + "%";
        }
    }
}