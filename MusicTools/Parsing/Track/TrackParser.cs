using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MusicTools.Utils;

namespace MusicTools.Parsing.Track
{
    public static class TrackParser
    {
        private static readonly Regex spotifyGay = new Regex("(.+) - ((.+) Remix)", RegexOptions.IgnoreCase);

        //TODO look at these
        // (ShockOne Instrumental Mix)
        // Follow You (Fractal Chill Mix) [feat. Danyka Nadeau]
        // Departed (Boy Kid Cloud VIP)
        // Alone (Streex Remake)
        // Hold Your Breath (Vorso Instrumental Mix)
        // Inside (Ekcle Edition)
        // Pomegranate (Carl Cox Dub Mix)

        private static readonly List<TrackPart> parts = new List<TrackPart>
        {
            new TrackPartStart("feat."), // (feat. XXX)
            new TrackPartStart("prod."), // (prod. XXX)
            new TrackPartEnd("Club Mix"), // (XXX Club Mix) BUG: Original Club Mix?
            new TrackPartEnd("'I Said It Again' ReEdit"), // (XXX 'I Said It Again' ReEdit)
            new TrackPartEnd("Re-Edit"), // (XXX Re-Edit)
            new TrackPartSkip("8 Minute Edit"), // (8 Minute Edit)
            new TrackPartSkip("2nd Edit"), // (2nd Edit)
            new TrackPartEnd("20:17 Edit"), // (XXX 20:17 Edit)
            new TrackPartEnd("DJ Edit"), // (XXX DJ Edit)
            new TrackPartEnd("Edit"), // (XXX Edit)
            new TrackPartEnd("VIP Remix"), // (XXX VIP Remix)
            new TrackPartEnd("'s Crushed Lyme Mix"), // (XXX's Crushed Lyme Mix)
            new TrackPartEnd("Extended Remix"), // (XXX Extended Remix)
            new TrackPartEnd("Extended Mix"), // (XXX Extended Mix)
            new TrackPartEnd("'s Pretentious Remix"), // (XXX's Pretentious Remix)
            new TrackPartEnd("'s Martian Trapstep Remix"), // (XXX's Martian Trapstep Remix)
            new TrackPartEnd("'s Matilda Remix"), // (XXX's Matilda Remix)
            new TrackPartEnd("'s 'Outer Edges' Remix"), // (XXX's 'Outer Edges' Remix)
            new TrackPartEnd("'s Thrillseeking Remix"), // (XXX's Thrillseeking Remix)
            new TrackPartEnd("2014 Remix"), // (XXX 2014 Remix)
            new TrackPartEnd("F No Remix"), // (XXX F No Remix)
            new TrackPartEnd("'s Dopest Dope Remix"), // (XXX's Dopest Dope Remix)
            new TrackPartEnd("'s Fear Is Weakness Remix"), // (XXX's Fear Is Weakness Remix)
            new TrackPartEnd("Driveclub Remix"), // (XXX Driveclub Remix)
            new TrackPartEnd("Droid Mix"), // (XXX Droid Mix)
            new TrackPartEnd("ReVibe"), // (XXX ReVibe)
            new TrackPartEnd("Flip"), // (XXX Flip)
            new TrackPartEnd("Reimagination"), // (XXX Reimagination)
            new TrackPartEnd("Bootleg"), // (XXX Bootleg)
            new TrackPartEnd("Rework"), // (XXX Rework)
            new TrackPartEnd("Rewire"), // (XXX Rewire)
            new TrackPartEnd("Remix"), // (XXX Remix)
            new TrackPartEnd("Extended Dub"), // (XXX Extended Dub)
            new TrackPartEnd("Dub"), // (XXX Dub)
        };

        //TODO make this less bad
        public static TrackInfo GetTrackInfo(string artists, string title, string albumArtists, string album, DateTime date)
        {
            var info = new TrackInfo
            {
                Artists = ArtistUtils.SplitArtists(artists),
                Features = new List<string>(),
                Remixers = new List<string>(),

                OriginalTitle = title,

                Album = album,
                AlbumArtists = ArtistUtils.SplitArtists(albumArtists),

                ScrobbledDate = date
            };

            var track = spotifyGay.Replace(title, "$1 ($3 Remix)");
            const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;

            try
            {
                var startIndex = 0;

                while (startIndex < track.Length && (startIndex = track.IndexOf("(", startIndex, COMPARISON_TYPE)) >= 0)
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
            }
            catch (Exception e)
            {
                Console.WriteLine($"error parsing '{info.OriginalTitle}': {e}");
            }

            info.ProcessedTitle = track.Trim();
            return info;
        }
    }

    internal class TrackPartSkip : TrackPart
    {
        private readonly string check;

        public TrackPartSkip(string str) { check = str; }

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

        public TrackPartStart(string str) { check = str; }

        internal override bool Process(TrackInfo info, ref int startIndex, ref int endIndex, ref string piece, StringComparison comparisonType)
        {
            if (startIndex + 1 + check.Length < piece.Length && string.Equals(piece.Substring(startIndex + 1, check.Length), check, comparisonType))
            {
                var featuresText = piece.Substring(startIndex + check.Length + 1, endIndex - startIndex - check.Length - 1)
                    .Trim();
                info.Features.AddRange(ArtistUtils.SplitArtists(featuresText));
                piece = piece.Remove(startIndex, endIndex - startIndex + 1)
                    .Trim();
                return true;
            }

            return false;
        }
    }

    internal class TrackPartEnd : TrackPart
    {
        private readonly string check;

        public TrackPartEnd(string str) { check = str; }

        internal override bool Process(TrackInfo info, ref int startIndex, ref int endIndex, ref string piece, StringComparison comparisonType)
        {
            if (endIndex - check.Length >= 0 && string.Equals(piece.Substring(endIndex - check.Length, check.Length), check, comparisonType))
            {
                var remixersText = piece.Substring(startIndex + 1, endIndex - startIndex - check.Length - 1)
                    .Trim();
                info.Remixers.AddRange(ArtistUtils.SplitArtists(remixersText));
                piece = piece.Remove(startIndex, endIndex - startIndex + 1)
                    .Trim();
                return true;
            }

            return false;
        }
    }

    internal abstract class TrackPart
    {
        internal abstract bool Process(TrackInfo info, ref int startIndex, ref int endIndex, ref string piece, StringComparison comparisonType);
    }
}