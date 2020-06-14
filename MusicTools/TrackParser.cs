using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicTools
{
    public static class TrackParser
    {
        private static readonly Regex spotifyGay = new Regex("(.+) - ((.+) Remix)", RegexOptions.IgnoreCase);

        private const string FEAT = "feat.";
        private const string PROD = "prod.";
        private const string EXTENDED_MIX = "Extended Mix";
        private const string EXTENDED_EDIT = "Extended Edit";
        private const string CLUB_MIX = "Club Mix";
        private const string CLUB_EDIT = "Club Edit";
        private const string ORIGINAL_CLUB_MIX = "Original Club Mix";
        private const string EDIT = "Edit";
        private const string I_SAID_IT_AGAIN_REEDIT = "'I Said It Again' ReEdit";
        private const string RE_EDIT = "Re-Edit";
        private const string RADIO_EDIT = "Radio Edit";
        private const string LIVE_EDIT = "Live Edit";
        private const string ALT_ENDING = "Alt. Ending";
        private const string AMBIENT_INTRO_MIX = "Ambient Intro Mix";
        private const string ORCHESTRAL_VERSION = "Orchestral Version";
        private const string REPRISE = "Reprise";
        private const string VIP = "VIP";
        private const string VIP_MIX = "VIP Mix";
        private const string VIP_REMIX = "VIP Remix";
        private const string ALT_VERSION_LC = "alt. version";
        private const string INSTRUMENTAL_MIX = "Instrumental Mix";
        private const string EXTENDED_INSTRUMENTAL_MIX = "Extended Instrumental Mix";
        private const string DROP_THE_POPTART_EDIT = "Drop The Poptart Edit";
        private const string CRUSHED_LYME_MIX = "'s Crushed Lyme Mix";
        private const string LYVE_AT_BRIXTON_EDIT = "Lyve at Brixton Edit";
        private const string REMIX = "Remix";
        private const string EXTENDED_REMIX = "Extended Remix";
        private const string EXTENDED_MIX2 = "Extended Mix";
        private const string PRETENTIOUS_REMIX = "'s 'Pretentious' Remix";
        private const string MARTIAN_TRAPSTEP_REMIX = "'s Martian Trapstep Remix";
        private const string MATILDA_REMIX = "'s Matilda Remix";
        private const string OUTER_EDGES_REMIX = "'s 'Outer Edges' Remix";
        private const string THRILLSEEKING_REMIX = "'s Thrillseeking Remix";
        private const string _2014_REMIX = "2014 Remix";
        private const string F_NO_REMIX = "F No Remix";
        private const string DOPEST_DOPE_REMIX = "'s Dopest Dope Remix";
        private const string FEAR_IS_WEAKNESS_REMIX = "'s Fear is Weakness Remix";
        private const string DRIVECLUB_REMIX = "Driveclub Remix";
        private const string DROID_MIX = "Droid Mix";
        private const string REVIBE = "ReVibe";
        private const string FLIP = "Flip";
        private const string REIMAGINATION = "Reimagination";
        private const string BOOTLEG = "Bootleg";
        private const string REWORK = "Rework";
        private const string REWIRE = "Rewire";
        private const string EXTENDED_DUB = "Extended Dub";
        private const string DUB = "Dub";
        private const string INTRO = "Intro";
        private const string OV = "ov";
        private const string ALT_VERSION = "Alt Version";
        private const string LISTEN_TO_TECHNO = "Listen to Techno";
        private const string INSTRUMENTAL = "Instrumental";
        private const string INTERLUDE = "Interlude";
        private const string CLOSE = "Close";
        private const string CLASSICAL = "Classical";

        // Idm Beat VIP
        // Sellout Mix
        // Original Vocal Mix
        // Drum and Bass VIP Mix
        // 2015 Remaster
        // Synthapella Version
        // Acoustic Mix
        // Kendall VIP
        // Pulser VIP
        // Melodic Synthwork VIP
        // Original Mix
        // Techno Radio Edit

        public static TrackInfo GetTrackInfo(string artists, string title, string albumArtists, string album,
            DateTime date)
        {
          var info = new TrackInfo
            {
                Artists = Artist.SplitArtists(artists),
                Features = new List<string>(),
                Remixers = new List<string>(),

                OriginalTitle = title,

                Album = album,
                AlbumArtists = Artist.SplitArtists(albumArtists),

                ScrobbledDate = date
            };

            var track = spotifyGay.Replace(title, "$1 ($3 Remix)");
            const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;

            try
            {
                int index = 0;
                while ((index = track.IndexOf("(",index, COMPARISON_TYPE)) >= 0)
                {
                    var endIndex = track.IndexOf(")", index, COMPARISON_TYPE);

                    if (index + 1 + FEAT.Length < track.Length &&
                        string.Equals(track.Substring(index + 1, FEAT.Length), FEAT, COMPARISON_TYPE))
                    {
                        var featuresText = track.Substring(index + FEAT.Length + 1, endIndex - index - FEAT.Length - 1)
                            .Trim();
                        info.Features.AddRange(Artist.SplitArtists(featuresText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + PROD.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, PROD.Length), PROD, COMPARISON_TYPE))
                    {
                        var featuresText = track.Substring(index + PROD.Length + 1, endIndex - index - PROD.Length - 1)
                            .Trim();
                        info.Features.AddRange(Artist.SplitArtists(featuresText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + EXTENDED_MIX.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), EXTENDED_MIX,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + EXTENDED_EDIT.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), EXTENDED_EDIT,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + ORIGINAL_CLUB_MIX.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), ORIGINAL_CLUB_MIX,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + CLUB_EDIT.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), CLUB_EDIT,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - CLUB_MIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - CLUB_MIX.Length, CLUB_MIX.Length), CLUB_MIX,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - CLUB_MIX.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + CLUB_MIX.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), CLUB_MIX, COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + RADIO_EDIT.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), RADIO_EDIT,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + LIVE_EDIT.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), LIVE_EDIT,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + DROP_THE_POPTART_EDIT.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), DROP_THE_POPTART_EDIT,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + LYVE_AT_BRIXTON_EDIT.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), LYVE_AT_BRIXTON_EDIT,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - I_SAID_IT_AGAIN_REEDIT.Length >= 0 &&
                             endIndex - I_SAID_IT_AGAIN_REEDIT.Length >= 0 &&
                             string.Equals(
                                 track.Substring(endIndex - I_SAID_IT_AGAIN_REEDIT.Length,
                                     I_SAID_IT_AGAIN_REEDIT.Length),
                                 I_SAID_IT_AGAIN_REEDIT, COMPARISON_TYPE))
                    {
                        var remixersText = track
                            .Substring(index + 1, endIndex - index - I_SAID_IT_AGAIN_REEDIT.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - RE_EDIT.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - RE_EDIT.Length, RE_EDIT.Length), RE_EDIT,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - RE_EDIT.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - EDIT.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - EDIT.Length, EDIT.Length), EDIT, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - EDIT.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + ALT_ENDING.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), ALT_ENDING,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + AMBIENT_INTRO_MIX.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), AMBIENT_INTRO_MIX,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + ORCHESTRAL_VERSION.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), ORCHESTRAL_VERSION,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + REPRISE.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), REPRISE, COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - VIP_REMIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - VIP_REMIX.Length, VIP_REMIX.Length), VIP_REMIX,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - VIP_REMIX.Length - 2).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + VIP_MIX.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), VIP_MIX, COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + VIP.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), VIP, COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + ALT_VERSION_LC.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), ALT_VERSION_LC,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + EXTENDED_INSTRUMENTAL_MIX.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), EXTENDED_INSTRUMENTAL_MIX,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + INSTRUMENTAL_MIX.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), INSTRUMENTAL_MIX,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - CRUSHED_LYME_MIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - CRUSHED_LYME_MIX.Length, CRUSHED_LYME_MIX.Length),
                                 CRUSHED_LYME_MIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - CRUSHED_LYME_MIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - EXTENDED_REMIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - EXTENDED_REMIX.Length, EXTENDED_REMIX.Length),
                                 EXTENDED_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - EXTENDED_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - EXTENDED_MIX2.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - EXTENDED_MIX2.Length, EXTENDED_MIX2.Length),
                                 EXTENDED_MIX2, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - EXTENDED_MIX2.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - PRETENTIOUS_REMIX.Length >= 0 &&
                             string.Equals(
                                 track.Substring(endIndex - PRETENTIOUS_REMIX.Length, PRETENTIOUS_REMIX.Length),
                                 PRETENTIOUS_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - PRETENTIOUS_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - MARTIAN_TRAPSTEP_REMIX.Length >= 0 &&
                             string.Equals(
                                 track.Substring(endIndex - MARTIAN_TRAPSTEP_REMIX.Length,
                                     MARTIAN_TRAPSTEP_REMIX.Length),
                                 MARTIAN_TRAPSTEP_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track
                            .Substring(index + 1, endIndex - index - MARTIAN_TRAPSTEP_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - MATILDA_REMIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - MATILDA_REMIX.Length, MATILDA_REMIX.Length),
                                 MATILDA_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - MATILDA_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - OUTER_EDGES_REMIX.Length >= 0 &&
                             string.Equals(
                                 track.Substring(endIndex - OUTER_EDGES_REMIX.Length, OUTER_EDGES_REMIX.Length),
                                 OUTER_EDGES_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - OUTER_EDGES_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - THRILLSEEKING_REMIX.Length >= 0 &&
                             string.Equals(
                                 track.Substring(endIndex - THRILLSEEKING_REMIX.Length, THRILLSEEKING_REMIX.Length),
                                 THRILLSEEKING_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - THRILLSEEKING_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - _2014_REMIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - _2014_REMIX.Length, _2014_REMIX.Length),
                                 _2014_REMIX,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - _2014_REMIX.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - F_NO_REMIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - F_NO_REMIX.Length, F_NO_REMIX.Length), F_NO_REMIX,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - F_NO_REMIX.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - DOPEST_DOPE_REMIX.Length >= 0 &&
                             string.Equals(
                                 track.Substring(endIndex - DOPEST_DOPE_REMIX.Length, DOPEST_DOPE_REMIX.Length),
                                 DOPEST_DOPE_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - DOPEST_DOPE_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - FEAR_IS_WEAKNESS_REMIX.Length >= 0 &&
                             string.Equals(
                                 track.Substring(endIndex - FEAR_IS_WEAKNESS_REMIX.Length,
                                     FEAR_IS_WEAKNESS_REMIX.Length),
                                 FEAR_IS_WEAKNESS_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track
                            .Substring(index + 1, endIndex - index - FEAR_IS_WEAKNESS_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - DRIVECLUB_REMIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - DRIVECLUB_REMIX.Length, DRIVECLUB_REMIX.Length),
                                 DRIVECLUB_REMIX, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - DRIVECLUB_REMIX.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - DROID_MIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - DROID_MIX.Length, DROID_MIX.Length), DROID_MIX,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - DROID_MIX.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - REVIBE.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - REVIBE.Length, REVIBE.Length), REVIBE,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - REVIBE.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - FLIP.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - FLIP.Length, FLIP.Length), FLIP, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - FLIP.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - REIMAGINATION.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - REIMAGINATION.Length, REIMAGINATION.Length),
                                 REIMAGINATION, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - REIMAGINATION.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - BOOTLEG.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - BOOTLEG.Length, BOOTLEG.Length), BOOTLEG,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - BOOTLEG.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - REWORK.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - REWORK.Length, REWORK.Length), REWORK,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - REWORK.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - REWIRE.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - REWIRE.Length, REWIRE.Length), REWIRE,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - REWIRE.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - REMIX.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - REMIX.Length, REMIX.Length), REMIX,
                                 COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - REMIX.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - EXTENDED_DUB.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - EXTENDED_DUB.Length, EXTENDED_DUB.Length),
                                 EXTENDED_DUB, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - EXTENDED_DUB.Length - 1)
                            .Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (endIndex - DUB.Length >= 0 &&
                             string.Equals(track.Substring(endIndex - DUB.Length, DUB.Length), DUB, COMPARISON_TYPE))
                    {
                        var remixersText = track.Substring(index + 1, endIndex - index - DUB.Length - 1).Trim();
                        info.Remixers.AddRange(Artist.SplitArtists(remixersText));
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + INTRO.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), INTRO,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + OV.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), OV,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + ALT_VERSION.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), ALT_VERSION,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + LISTEN_TO_TECHNO.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), LISTEN_TO_TECHNO,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + INSTRUMENTAL.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), INSTRUMENTAL,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + INTERLUDE.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), INTERLUDE,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + CLOSE.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), CLOSE,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else if (index + 1 + CLASSICAL.Length < track.Length &&
                             string.Equals(track.Substring(index + 1, endIndex - index - 1), CLASSICAL,
                                 COMPARISON_TYPE))
                    {
                        track = track.Remove(index, endIndex - index + 1).Trim();
                    }
                    else
                    {
                        Console.WriteLine($"!!!: {track.Substring(index + 1, endIndex - index - 1)}");
                        index = endIndex;
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
}