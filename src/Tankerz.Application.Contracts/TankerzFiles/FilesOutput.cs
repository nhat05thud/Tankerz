using System;

namespace Tankerz.TankerzFiles
{
    public class FilesOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageSmallUrl { get; set; }
        public string Extension { get; set; }
        public bool Checked { get; set; }
    }
}
