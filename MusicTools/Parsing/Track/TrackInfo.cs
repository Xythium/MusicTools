using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicTools.Parsing.Track;

public class TrackInfo : IEquatable<TrackInfo>
{
    public List<string> Artists { get; set; }

    public List<string> Features { get; set; }

    public List<string> Remixers { get; set; }

    public string OriginalTitle { get; set; }

    public string ProcessedTitle { get; set; }

    public string RemixName { get; set; }

    public string Album { get; set; }

    public List<string> AlbumArtists { get; set; }

    public DateTime ScrobbledDate { get; set; }

    public bool Equals(TrackInfo other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;

        return Artists.SequenceEqual(other.Artists) && 
               Features.SequenceEqual(other.Features) &&
               Remixers.SequenceEqual(other.Remixers) &&
               string.Equals(ProcessedTitle, other.ProcessedTitle, StringComparison.OrdinalIgnoreCase) && 
               string.Equals(RemixName, other.RemixName, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(Album, other.Album, StringComparison.OrdinalIgnoreCase) &&
               AlbumArtists.SequenceEqual(other.AlbumArtists) &&
               ScrobbledDate.Equals(other.ScrobbledDate);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != this.GetType())
            return false;

        return Equals((TrackInfo)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (Artists != null ? Artists.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Features != null ? Features.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Remixers != null ? Remixers.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (ProcessedTitle != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(ProcessedTitle) : 0);
            hashCode = (hashCode * 397) ^ (RemixName != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(RemixName) : 0);
            hashCode = (hashCode * 397) ^ (Album != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Album) : 0);
            hashCode = (hashCode * 397) ^ (AlbumArtists != null ? AlbumArtists.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ ScrobbledDate.GetHashCode();
            return hashCode;
        }
    }
}