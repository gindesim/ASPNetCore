using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using DoruReactAPICore22.Models;

namespace DoruReactAPICore22.Shared
{
    public static class ReadWriteFile
    {
        public static void WriteJson(string jsonString, string filePath)
        {
            JArray jsonArray = JArray.Parse(jsonString);
            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(filePath))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                jsonArray.WriteTo(writer);
            }
        }

        public static string ReadJson(string filePath)
        {
            string result;

            // read JSON directly to a file
            using (StreamReader file = File.OpenText(filePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray jsonArray = (JArray)JToken.ReadFrom(reader);
                result = JsonConvert.SerializeObject(jsonArray);
            }
            return result;
        }

        public static string ReadTextAsJson(string filePath)
        {
            string result = null;
            string[] lines, splitLine;
            string subLine;
            List<CoverModel> coverList = new List<CoverModel>();

            //result = File.ReadAllText(filePath);
            lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                subLine = line.Length > 40 ? line.Substring(39) : "";
                splitLine = subLine.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int splitCount = 0;
                splitCount = splitLine.Count();
                if (splitCount > 1 && splitLine[--splitCount] == "jpg")
                {
                    CoverModel cover = new CoverModel();
                    cover.Filename = subLine;
                    coverList.Add(cover);
                }
            }
            ParseCoverFilename(coverList);
            result = JsonConvert.SerializeObject(coverList);
            return result;
        }

        public static void ParseCoverFilename(List<CoverModel> covers)
        {
            string[] nameSplit;
            string covercast = "", coverseries, coverlabel;
            DateTime reldate;

            int splitCount = 0;
            foreach (CoverModel cover in covers)
            {
                nameSplit = cover.Filename.Split(new char[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                splitCount = nameSplit.Count();
                if (splitCount > 1 && nameSplit[--splitCount] == "jpg")
                {
                    if (Common.IsStringDateTime(nameSplit[--splitCount], out reldate))
                    {
                        cover.Releasedate = $"{reldate.ToString("yyyy")}-{reldate.ToString("MM")}-{reldate.ToString("dd")}";
                        --splitCount;
                    }
                    covercast = "";
                    for (; splitCount > 0; --splitCount)
                    {
                        covercast = $" {nameSplit[splitCount]}{covercast}";
                    }
                    cover.Cast = covercast?.Trim();
                    if (Common.IsStringCoverSeries(nameSplit[0], out coverseries, out coverlabel))
                    {
                        cover.Series = coverseries?.Trim();
                    }
                    if (coverlabel != null)
                        cover.Label = coverlabel?.Trim();
                }
            }
        }

    }
}

