using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using DoruCore22.Models;

namespace DoruCore22.Shared
{
    public static class Common
    {
        public static bool IsStringCoverSeries(string tryString, out string coverseries)
        {
            bool result = false;
            string[] coversplit;
            coverseries = "";
            coversplit = tryString.Split("-", StringSplitOptions.RemoveEmptyEntries);
            if (coversplit.Count() == 2)
            {
                int numeric;
                bool isnumeric = IsStringNumeric(coversplit[1], out numeric);
                if(isnumeric)
                {
                    coverseries = $"{coversplit[0].Trim()}-{coversplit[1].Trim()}";
                    result = true;
                }
            }
            return result;
        }

        public static bool IsStringNumeric(string tryString, out int numeric)


        {
            bool result;
            result = int.TryParse(tryString, out numeric);
            return result;
        }

        public static bool IsStringDateTime(string tryString, out DateTime datetime)
        {
            bool result;
            result = DateTime.TryParse(tryString, out datetime);
            return result;
        }
    }
}

