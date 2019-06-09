using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace DoruCore22.Models
{
    public class CastModel
    {
        public long Id { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public IEnumerable<CoverModel> covers { get; set; }
    }
}
