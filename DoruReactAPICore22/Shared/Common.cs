using System;
using System.Linq;

namespace DoruReactAPICore22.Shared
{
    public static class Common
    {
        public static bool IsStringCoverSeries(string tryString, out string series, out string label)
        {
            bool result = false;
            string[] nameSplit;
            int numeric;
            series = "";
            label = null;

            nameSplit = tryString.Split("-", StringSplitOptions.RemoveEmptyEntries);
            int splitCount = nameSplit.Count();

            label = splitCount == 2 ? nameSplit[0] : label;
            bool isnumeric = IsStringNumeric(nameSplit[--splitCount], out numeric);
            if (isnumeric)
            {
                series = tryString?.Trim();
                result = true;
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

