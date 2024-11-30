using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MusicTools.Utils;

namespace MusicTools.Parsing.Track;

public static class TrackParser
{
    private static readonly Regex spotifyBad = new Regex("(.+) - ((.+) Remix)", RegexOptions.IgnoreCase);

    //TODO: look at these
    // (ShockOne Instrumental Mix)
    // Follow You (Fractal Chill Mix) [feat. Danyka Nadeau]
    // Departed (Boy Kid Cloud VIP)
    // Hold Your Breath (Vorso Instrumental Mix)

    private static readonly List<TrackPart> parts =
    [
        new TrackPartStart("feat."), // (feat. XXX)
        new TrackPartStart("prod."), // (prod. XXX)
        new TrackPartEnd("Dub Mix"), // (XXX Dub Mix)
        new TrackPartSkip("Original Club Mix"), // (Original Club Mix)
        new TrackPartSkip("Instrumental Mix"), // (Instrumental Mix)
        new TrackPartEnd("Club Mix"), // (XXX Club Mix)
        new TrackPartEnd("'I Said It Again' ReEdit", "'I Said It Again'"), // (XXX 'I Said It Again' ReEdit)
        new TrackPartEnd("Re-Edit"), // (XXX Re-Edit)
        //  new TrackPartSkip("Afterhours Mix"), // (Afterhours Mix)
        new TrackPartSkip("Extended Edit"), // (Extended Edit)
        new TrackPartSkip("Broken Edit"), // (Broken Edit)
        new TrackPartSkip("Radio Edit"), // (Radio Edit)
        new TrackPartSkip("Club Edit"), // (Club Edit)
        new TrackPartSkip("8 Minute Edit"), // (8 Minute Edit)
        new TrackPartSkip("2nd Edit"), // (2nd Edit)
        new TrackPartEnd("20:17 Edit"), // (XXX 20:17 Edit)
        new TrackPartEnd("DJ Edit"), // (XXX DJ Edit)
        new TrackPartEnd("Radio Edit"), // (XXX Radio Edit)
        new TrackPartEnd("Edit"), // (XXX Edit)
        new TrackPartEnd("VIP Remix"), // (XXX VIP Remix)
        new TrackPartEnd("VIP"), // (XXX VIP)
        new TrackPartEnd("'s Crushed Lyme Mix"), // (XXX's Crushed Lyme Mix)
        new TrackPartEnd("20:17 Remix"), // (XXX 20:17 Remix)
        new TrackPartEnd("Extended Remix"), // (XXX Extended Remix)
        new TrackPartEnd("Extended Mix"), // (XXX Extended Mix)
        new TrackPartEnd("'s Pretentious Remix", "Pretentious"), // (XXX's Pretentious Remix)
        new TrackPartEnd("'s Martian Trapstep Remix", "Martian Trapstep"), // (XXX's Martian Trapstep Remix)
        new TrackPartEnd("'s Matilda Remix", "Matilda"), // (XXX's Matilda Remix)
        new TrackPartEnd("'s 'Outer Edges' Remix", "'Outer Edges'"), // (XXX's 'Outer Edges' Remix)
        new TrackPartEnd("'s Thrillseeking Remix", "Thrillseeking"), // (XXX's Thrillseeking Remix)
        new TrackPartEnd("2014 Remix", "2014"), // (XXX 2014 Remix)
        new TrackPartEnd("F No Remix", "F No"), // (XXX F No Remix)
        new TrackPartEnd("'s Dopest Dope Remix", "Dopest Dope"), // (XXX's Dopest Dope Remix)
        new TrackPartEnd("'s Fear Is Weakness Remix", "Fear Is Weakness"), // (XXX's Fear Is Weakness Remix)
        new TrackPartEnd("Driveclub Remix", "Driveclub"), // (XXX Driveclub Remix)
        new TrackPartEnd("Alternative Remix", "Alternative"), // (XXX Alternative Remix)
        new TrackPartEnd("Droid Mix"), // (XXX Droid Mix)
        new TrackPartEnd("ReVibe"), // (XXX ReVibe)
        new TrackPartEnd("Flip"), // (XXX Flip)
        new TrackPartEnd("Reimagination"), // (XXX Reimagination)
        new TrackPartEnd("Bootleg"), // (XXX Bootleg)
        new TrackPartEnd("Rework - Edit"), // (XXX Rework - Edit)
        new TrackPartEnd("Rework"), // (XXX Rework)
        new TrackPartEnd("Rewire"), // (XXX Rewire)
        new TrackPartEnd("Remix"), // (XXX Remix)
        new TrackPartEnd("Remake"), // (XXX Remake)
        new TrackPartEnd("Edition"), // (XXX Edition) //todo: maybe sketchy
        new TrackPartEnd("Old School Deconstruction"), // (XXX Old School Deconstruction)
        new TrackPartEnd("Extended Dub"), // (XXX Extended Dub)
        new TrackPartEnd("Dub"), // (XXX Dub)
        new TrackPartEnd("Instrumental Mix") // (XXX Instrumental Mix)
    ];

    private static Dictionary<int, TrackInfo> cache = new Dictionary<int, TrackInfo>();

    //TODO make this less bad
    public static TrackInfo GetTrackInfo(string artists, string title, string albumArtists, string album, DateTime date)
    {
        var key = (artists + title + albumArtists + album).GetHashCode();

        if (cache.TryGetValue(key, out var cached))
        {
            return cached;
        }

        var info = new TrackInfo
        {
            Artists = ArtistUtils.SplitArtists(artists),
            Features = [],
            Remixers = [],

            OriginalTitle = title,

            Album = album,
            AlbumArtists = ArtistUtils.SplitArtists(albumArtists),

            ScrobbledDate = date
        };

        var track = spotifyBad.Replace(title, "$1 ($3 Remix)"); //todo: slow, make this work like it does with brackets
        const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;

        try
        {
            var startIndex = 0;

            while (startIndex < track.Length /*&& startIndex >= 0 */ && (startIndex = track.IndexOf("(", startIndex, COMPARISON_TYPE)) >= 0)
            {
                var endIndex = track.IndexOf(")", startIndex, COMPARISON_TYPE);

                if (endIndex < startIndex)
                {
                    Console.WriteLine($"error parsing '{info.OriginalTitle}': malformed brackets");
                    startIndex += 1;
                }

                var noMatch = true;

                foreach (var part in parts)
                {
                    var found = part.Process(info, ref startIndex, ref endIndex, ref track, COMPARISON_TYPE);

                    if (found)
                    {
                        noMatch = false;
                        break;
                    }
                }

                if (noMatch)
                {
                    Console.WriteLine($"info parsing '{info.OriginalTitle}': unknown pattern");
                    startIndex = endIndex;
                }
            }

            startIndex = 0;

            while (startIndex < track.Length && (startIndex = track.IndexOf("[", startIndex, COMPARISON_TYPE)) >= 0)
            {
                var endIndex = track.IndexOf("]", startIndex, COMPARISON_TYPE);

                if (endIndex < startIndex)
                {
                    Console.WriteLine($"error parsing '{info.OriginalTitle}': malformed brackets");
                    startIndex += 1;
                }

                var noMatch = true;

                foreach (var part in parts)
                {
                    var found = part.Process(info, ref startIndex, ref endIndex, ref track, COMPARISON_TYPE);

                    if (found)
                    {
                        noMatch = false;
                        //break; //todo figure out why i did this
                    }
                }

                if (noMatch)
                {
                    Console.WriteLine($"info parsing '{info.OriginalTitle}': unknown pattern");
                    startIndex = endIndex;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"error parsing '{info.OriginalTitle}': {e}");
        }

        info.ProcessedTitle = track.Trim();

        cache.Add(key, info);

        return info;
    }
}

internal class TrackPartSkip : TrackPart
{
    private readonly string check;

    public TrackPartSkip(string str)
    {
        check = str;
    }

    internal override bool Process(TrackInfo info, ref int startIndex, ref int endIndex, ref string piece, StringComparison comparisonType)
    {
        if (startIndex + 1 + check.Length < piece.Length && string.Equals(piece.Substring(startIndex + 1, check.Length), check, comparisonType))
        {
            startIndex = endIndex;
            return true;
        }

        return false;
    }
}

internal class TrackPartStart : TrackPart
{
    private readonly string check;

    public TrackPartStart(string str)
    {
        check = str;
    }

    internal override bool Process(TrackInfo info, ref int startIndex, ref int endIndex, ref string piece, StringComparison comparisonType)
    {
        if (startIndex + 1 + check.Length < piece.Length && string.Equals(piece.Substring(startIndex + 1, check.Length), check, comparisonType))
        {
            var featuresText = piece.Substring(startIndex + check.Length + 1, endIndex - startIndex - check.Length - 1).Trim();
            info.Features.AddRange(ArtistUtils.SplitArtists(featuresText));
            piece = piece.Remove(startIndex, endIndex - startIndex + 1).Trim();
            return true;
        }

        return false;
    }
}

internal class TrackPartEnd : TrackPart
{
    private readonly string check;
    private readonly string name;

    public TrackPartEnd(string str, string name = "")
    {
        check = str;
        this.name = name;
    }

    internal override bool Process(TrackInfo info, ref int startIndex, ref int endIndex, ref string piece, StringComparison comparisonType)
    {
        if (endIndex - check.Length >= 0 && string.Equals(piece.Substring(endIndex - check.Length, check.Length), check, comparisonType))
        {
            var remixersText = piece.Substring(startIndex + 1, endIndex - startIndex - check.Length - 1).Trim();
            info.Remixers.AddRange(ArtistUtils.SplitArtists(remixersText));
            info.RemixName = name;
            piece = piece.Remove(startIndex, endIndex - startIndex + 1).Trim();
            return true;
        }

        return false;
    }
}

internal abstract class TrackPart
{
    internal abstract bool Process(TrackInfo info, ref int startIndex, ref int endIndex, ref string piece, StringComparison comparisonType);
}