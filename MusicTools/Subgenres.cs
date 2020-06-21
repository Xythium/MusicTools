using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicTools
{
    public static class Subgenres
    {
        private static readonly Regex subgenreRegex = new Regex("/ | > | \\| ", RegexOptions.IgnoreCase);

        public static List<string> SplitSubgenres(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            text = text.Replace("(", "")
                .Replace(")", "");

            var artists = new List<string>();
            var split = subgenreRegex.Split(text);

            for (var i = 0; i < split.Length; i++)
            {
                var artist = split[i];
                if (string.IsNullOrWhiteSpace(artist))
                    continue;

                artist = artist.Trim();
                artists.Add(artist);
            }

            return artists;
        }
    }
}