using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicTools
{
    public static class Artist
    {
        private const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;

        private static readonly Regex artistRegex =
            new Regex(", & | & | x |, | vs. | vs | and ", RegexOptions.IgnoreCase);

        private static readonly HashSet<string> splitExceptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Sailor & I",
            "J Majik & Wickaman",
            "nothing,nowhere.",
            "Camo & Krooked",
            "Simon & Garfunkel",
            "Jos & Eli",
            "Ost & Kjex",
            "Suga & Uner",
            "Kraak & Smaak",
            "Sidewalks and Skeletons",
            "Above & Beyond",
            "Blunts & Blondes",
            "Chase & Status",
            "aivi & surasshu",
            "Dodge & Fuski",
            "Chase & Point",
            "Gent & Jawns"
        };

        public static List<string> SplitArtists(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return new List<string>();

            var artists = new List<string>();
            var split = artistRegex.Split(text);

            for (var i = 0; i < split.Length; i++)
            {
                var artist = split[i];
                if (string.IsNullOrWhiteSpace(artist)) continue;

                var next = i + 1 < split.Length ? split[i + 1] : null;

                if (splitExceptions.Contains(artist + " & " + next))
                {
                    artists.Add(artist + " & " + next);
                    i++;
                }
                else if (splitExceptions.Contains(artist + " and " + next))
                {
                    artists.Add(artist + " and " + next);
                    i++;
                }
                else
                {
                    artists.Add(artist);
                }
            }

            return artists;
        }

        public static string ForceCasing(string s)
        {
            return
                s.Replace("Mr. Bill", "Mr. Bill")
                    .Replace("KUURO", "KUURO")
                    .Replace("Mad Zach", "Mad Zach")
                    .Replace("EDDIE", "EDDIE")
                    .Replace("Vorso", "Vorso")
                    .Replace("yunis", "yunis")
                    .Replace("CHEE", "CHEE")
                    .Replace("REZZ", "REZZ")
                    .Replace("i_o", "i_o")
                    .Replace("HEYZ", "HEYZ")
                    .Replace("PROKO", "PROKO")
                    .Replace("SKEW", "SKEW")
                    .Replace("ZEKE BEATS", "ZEKE BEATS")
                    .Replace("EPROM", "EPROM")
                    .Replace("G Jones", "G Jones")
                    .Replace("k?d", "k?d")
                    .Replace("MOGUAI", "MOGUAI")
                    .Replace("Monstergetdown", "Monstergetdown") // Monstergetdown?
                    .Replace("Rhett", "Rhett")
                    .Replace("Rinzen", "Rinzen")
                    .Replace("TESTPILOT", "TESTPILOT")
                    .Replace("deadmau5", "deadmau5");
        }
    }
}