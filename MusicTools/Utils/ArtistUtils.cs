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
            "Jkyl & Hyde",
            "Brown & Gammon",
            "Milo & Otis"
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

        // a lot of these are weird Last.Fm cases
        private static readonly HashSet<string> artistCasingExceptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "EDDIE",
            "Mr. Bill",
            "KUURO",
            "Mad Zach",
            "Vorso",
            "yunis",
            "Chee", // todo: CHEE or Chee?
            "i_o",
            "HEYZ",
            "PROKO",
            "SKEW",
            "ZEKE BEATS",
            "EPROM",
            "G Jones",
            "k?d",
            "MOGUAI",
            "Monstergetdown",
            "Rhett",
            "Rinzen",
            "TESTPILOT",
            "BLAIR ROUGE",
            "IMANU",
            "deadmau5",
            "COPYCATT",
            "Conro",
            "Run DMT",
            "Monuman",
            "Vellum",
            "REZZ",
            "JOYRYDE",
            "JUMPBEAR",
            "donkfloor",
            "OaT",
            "Ding",
            "Bleep Bloop",
            "BLiSS",
            "RefraQ",
            "BYOR",
            "Kindrid",
            "Morelia",
            "ALEPH",
            "REAPER",
            "BOATS",
            "ASHE",
            "FLIP-FLOP",
            "KEETZ",
            "ATTLAS",
            "SOFI",
        };

        public static string ForceCasing(string s)
        {
#if NETSTANDARD2_0 //BUG: dunno what is going on here
            foreach (var x in artistCasingExceptions)
            {
                s = s.Replace(s, x);
            }
#else
            if (artistCasingExceptions.TryGetValue(s, out var name))
            {
                s = s.Replace(s, name);
            }
#endif

            return s;
        }
    }
}