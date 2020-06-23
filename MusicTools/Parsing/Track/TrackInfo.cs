using System;
using System.Collections.Generic;

namespace MusicTools.Parsing.Track
{
    public struct TrackInfo
    {
        public List<string> Artists { get; set; }

        public List<string> Features { get; set; }

        public List<string> Remixers { get; set; }

        public string OriginalTitle { get; set; }

        public string ProcessedTitle { get; set; }

        public string Album { get; set; }

        public List<string> AlbumArtists { get; set; }

        public DateTime ScrobbledDate { get; set; }
    }
}