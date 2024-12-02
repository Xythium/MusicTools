using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTools.Parsing.Track;
using NUnit.Framework;
using VerifyNUnit;
using VerifyTests;

namespace MusicToolTests
{
    public class TrackParserOldTests
    {
        private VerifySettings settings;

        [OneTimeSetUp]
        public void Setup()
        {
            settings = new VerifySettings();
            settings.UseDirectory("snapshots");
        }

        private readonly List<string> emptyStringList = new List<string>();

        [Test, TestCaseSource(typeof(TrackParserOldCases), nameof(TrackParserOldCases.TestCases))]
        public Task Title(string artist, string title)
        {
            var info = TrackParser.GetTrackInfo(artist, title, "", "", DateTime.Now);
            return Verifier.Verify(info, settings);
        }
    }
}

public class TrackParserOldCases
{
    public static IEnumerable TestCases
    {
        get
        {
            yield return new TestCaseData("EDDIE", "Somewhere in Between (feat. Colleen D'Agostino) [Extended Mix]");
            yield return new TestCaseData("EDDIE", "Somewhere in Between (feat. Colleen D'Agostino) (Extended Mix)");
            yield return new TestCaseData("yunis", "Limbic (Value of Stimuli)");
            yield return new TestCaseData("Subp Yao", "Lost (Floating) (feat. Astronautalis)");
            yield return new TestCaseData("i_o & LIGHTS", "Annihilation (Afterhours Mix)");
            yield return new TestCaseData("Ty Dolla $ign", "Ego Death (feat. Kanye West, FKA twigs, & Skrillex)");
            yield return new TestCaseData("Monstergetdown", "maybe nothing (Extended Edit)");
            yield return new TestCaseData("BUDD", "Triangular (Radio Edit)");
            yield return new TestCaseData("deadmau5", "Strobe (Club Edit)");
            yield return new TestCaseData("Mystic State", "Ever More (War Remix)");
            yield return new TestCaseData("Zeds Dead x Loge21", "Just Wanna (sumthin sumthin Remix) [feat. Polina]");
            yield return new TestCaseData("deadmau5 & The Neptunes", "Pomegranate (Ninajirachi Extended Remix)");
            yield return new TestCaseData("Romare", "All Night (Karma Fields Edit)");
            yield return new TestCaseData("Wingz", "I Got (Vorso 20:17 Remix)");
            yield return new TestCaseData("MOGUAI x Rebecca & Fiona", "Sad Boy, Happy Girl");
            yield return new TestCaseData("MOGUAI & Rebecca & Fiona", "Sad Boy, Happy Girl");

        }
    }
}