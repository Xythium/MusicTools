using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicTools.Utils;

public static class ArtistUtils
{
    private const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;

    private static readonly Regex artistRegex = new Regex(", & | & | x |, | vs. | vs | and | ✘ | feat. ", RegexOptions.IgnoreCase);

    private static readonly HashSet<string> splitExceptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "Above & Beyond",
        "aivi & surasshu",
        "Blunts & Blondes",
        "Brown & Gammon",
        "Camo & Krooked",
        "Case & Point",
        "Chase & Status",
        "Cosa & Kay",
        "Dodge & Fuski",
        "Gent & Jawns",
        "Jkyl & Hyde",
        "Jos & Eli",
        "Kraak & Smaak",
        "Milo & Otis",
        "nothing,nowhere.",
        "Ost & Kjex",
        "Rebecca & Fiona",
        "Sailor & I",
        "Sidewalks and Skeletons",
        "Simon & Garfunkel",
        "Suga & Uner",
        "Slips & Slurs",
        "T & Sugah",
        "Gabriel & Dresden"
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
        "ALEPH",
        "ARKTKT",
        "ASHE",
        "ATTLAS",
        "BAR9",
        "BLAIR ROUGE",
        "Bleep Bloop",
        "BLiSS",
        "BOATS",
        "BUDD",
        "BYOR",
        "Chee", // todo: CHEE or Chee?
        "Conro",
        "COPYCATT",
        "darkDARK",
        "deadmau5",
        "Ding",
        "DJ RIDE",
        "donkfloor",
        "EDDIE",
        "EPROM",
        "FLIP-FLOP",
        "G Jones",
        "HEYZ",
        "HØST",
        "HvrdLxck",
        "i_o",
        "IMANU",
        "JOYRYDE",
        "JUMPBEAR",
        "k?d",
        "KEETZ",
        "Kindrid",
        "KUURO",
        "LICK",
        "LIGHTS",
        "Liquid Stranger",
        "LWK",
        "Mad Zach",
        "MAERE",
        "MISSIN",
        "MOGUAI",
        "Monstergetdown",
        "Monuman",
        "Morelia",
        "Moscoman",
        "Mr. Bill",
        "MRSA",
        "MUADEEP",
        "MUZZ",
        "NemesiZ",
        "NEUS",
        "no puls",
        "NOISEFLOOR",
        "OaT",
        "OCULA",
        "PROKO",
        "REAPER",
        "Redpill",
        "RefraQ",
        "REZZ",
        "Rhett",
        "Rinzen",
        "Rizzle",
        "Run DMT",
        "Shadient",
        "SKEW",
        "SOFI",
        "Stonebank",
        "TESTPILOT",
        "Vellum",
        "Vorso",
        "x_x",
        "yunis",
        "ZEKE BEATS",
        "RBSH",
        "ÆTERNA",
        "ABIS",
        "DotCrawL",
        "Tinlicker"
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