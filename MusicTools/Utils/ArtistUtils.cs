using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicTools.Utils
{
    public static class ArtistUtils
    {
        private const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;

        private static readonly Regex artistRegex = new Regex(", & | & | x |, | vs. | vs | and ", RegexOptions.IgnoreCase);

        private static readonly HashSet<string> splitExceptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Sailor & I",
            //"J Majik & Wickaman",
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
            "Case & Point",
            "Chase & Status",
            "Gent & Jawns",
            "T & Sugah",
            "Jkyl & Hyde"
        };

        public static List<string> SplitArtists(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            var artists = new List<string>();
            var split = artistRegex.Split(text);

            for (var i = 0; i < split.Length; i++)
            {
                var artist = split[i];
                if (string.IsNullOrWhiteSpace(artist))
                    continue;

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
                    artists.Add(ForceCasing(artist));
                }
            }

            return artists;
        }

        public static string ForceCasing(string s)
        {
#if NETCOREAPP
            return s.Replace("Mr. BIll", "Mr. Bill", COMPARISON_TYPE)
                .Replace("Kuuro", "KUURO", COMPARISON_TYPE)
                .Replace("MAD ZACH", "Mad Zach", COMPARISON_TYPE)
                .Replace("Eddie", "EDDIE", COMPARISON_TYPE)
                .Replace("vorso", "Vorso", COMPARISON_TYPE)
                .Replace("Yunis", "yunis", COMPARISON_TYPE)
                .Replace("Chee", "CHEE", COMPARISON_TYPE)
                .Replace("Rezz", "REZZ", COMPARISON_TYPE)
                .Replace("I_O", "i_o", COMPARISON_TYPE)
                .Replace("HEYz", "HEYZ", COMPARISON_TYPE)
                .Replace("proko", "PROKO", COMPARISON_TYPE)
                .Replace("Skew", "SKEW", COMPARISON_TYPE)
                .Replace("Zeke Beats", "ZEKE BEATS", COMPARISON_TYPE)
                .Replace("Eprom", "EPROM", COMPARISON_TYPE)
                .Replace("G JONES", "G Jones", COMPARISON_TYPE)
                .Replace("K?D", "k?d", COMPARISON_TYPE)
                .Replace("Moguai", "MOGUAI", COMPARISON_TYPE)
                .Replace("monstergetdown", "Monstergetdown", COMPARISON_TYPE)
                .Replace("rhett", "Rhett", COMPARISON_TYPE)
                .Replace("RINZEN", "Rinzen", COMPARISON_TYPE)
                .Replace("testpilot", "TESTPILOT", COMPARISON_TYPE)
                .Replace("Attlas", "ATTLAS", COMPARISON_TYPE)
                .Replace("Blair Rouge", "BLAIR ROUGE", COMPARISON_TYPE)
                .Replace("Imanu", "IMANU", COMPARISON_TYPE)
                .Replace("Deadmau5", "deadmau5", COMPARISON_TYPE);
#else
             return s.Replace("Mr. BIll", "Mr. Bill")
                .Replace("Kuuro", "KUURO")
                .Replace("MAD ZACH", "Mad Zach")
                .Replace("Eddie", "EDDIE")
                .Replace("vorso", "Vorso")
                .Replace("Yunis", "yunis")
                .Replace("Chee", "CHEE")
                .Replace("Rezz", "REZZ")
                .Replace("I_O", "i_o")
                .Replace("HEYz", "HEYZ")
                .Replace("proko", "PROKO")
                .Replace("Proko", "PROKO")
                .Replace("Skew", "SKEW")
                .Replace("Zeke Beats", "ZEKE BEATS")
                .Replace("Eprom", "EPROM")
                .Replace("G JONES", "G Jones")
                .Replace("K?D", "k?d")
                .Replace("Moguai", "MOGUAI")
                .Replace("monstergetdown", "Monstergetdown")
                .Replace("rhett", "Rhett")
                .Replace("RINZEN", "Rinzen")
                .Replace("testpilot", "TESTPILOT")
                .Replace("Attlas", "ATTLAS")
                .Replace("Blair Rouge", "BLAIR ROUGE")
                .Replace("Imanu", "IMANU")
                .Replace("Deadmau5", "deadmau5");
#endif
        }
    }
}