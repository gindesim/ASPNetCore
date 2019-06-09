using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace DoruCore22.Models
{
    public class CoverModel
    {
        public long Id { get; set; }

        public string filename { get; set; }

        public string label { get; set; }

        public string series { get; set; }

        public string cast { get; set; }

        public string releasedate { get; set; }

        public string folder { get; set; }
    }
}
