﻿using System.IO;
using System.Linq;

namespace MoviePotato.Models
{
    public class Video
    {
        public string FileName { get; set; }
        public string Location { get; set; }
        public bool Watched { get; set; }

        private string Path
        {
            get
            {
                return Directory.GetFiles(Location, "*" + FileName + "*", SearchOption.AllDirectories).FirstOrDefault();
            }
        }

        public bool HasLocalCopy
        {
            get
            {
                return Path != null;
            }
        }

        public bool TryPlay()
        {
            if (!HasLocalCopy)
            {
                return false;
            }
            else
            {
                System.Diagnostics.Process.Start(Path);
                return true;
            }
        }
    }
}