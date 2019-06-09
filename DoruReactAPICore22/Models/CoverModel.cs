using System;
using System.Collections.Generic;

namespace DoruReactAPICore22.Models
{
    public partial class CoverModel
    {
        public long Id { get; set; }
        public string Filename { get; set; }
        public string Label { get; set; }
        public string Series { get; set; }
        public string Cast { get; set; }
        public string Releasedate { get; set; }
        public string Folder { get; set; }
    }
}
