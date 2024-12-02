using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicTools.Parsing.Track;
using NUnit.Framework;
using VerifyNUnit;

namespace MusicToolTests
{
    
    [TestFixture]
    public class VerifyChecksTests
    {
        [Test]
        public Task Run() =>
            VerifyChecks.Run();
    }
    
    public class Tests
    {
        [SetUp]
        public void Setup() { }

        private readonly List<string> emptyStringList = new List<string>();

        [Test]
        public void TrackParser_Extended_Mix()
        {
            var info = TrackParser.GetTrackInfo("EDDIE", "Somewhere in Between (feat. Colleen D'Agostino) [Extended Mix]", "EDDIE", "Somewhere in Between", DateTime.Now);
            Assert.That(new List<string>
            {
                "EDDIE"
            }, Is.EqualTo(info.Artists));
            Assert.That(new List<string>
            {
                "Colleen D'Agostino"
            }, Is.EqualTo(info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Somewhere in Between", Is.EqualTo(info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Extended_Mix2()
        {
            var info = TrackParser.GetTrackInfo("EDDIE", "Somewhere in Between (feat. Colleen D'Agostino) (Extended Mix)", "EDDIE", "Somewhere in Between", DateTime.Now);
            Assert.That(new List<string>
            {
                "EDDIE"
            },Is.EqualTo( info.Artists));
            Assert.That(new List<string>
            {
                "Colleen D'Agostino"
            },Is.EqualTo( info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Somewhere in Between", Is.EqualTo(info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Title()
        {
            var info = TrackParser.GetTrackInfo("yunis", "Limbic (Value of Stimuli)", "yunis", "Amygdala", DateTime.Now);
            Assert.That(new List<string>
            {
                "yunis"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList,Is.EqualTo(info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Limbic (Value of Stimuli)",Is.EqualTo( info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Title2()
        {
            var info = TrackParser.GetTrackInfo("Subp Yao", "Lost (Floating) (feat. Astronautalis)", "Subp Yao", "Infra Aqual", DateTime.Now);
            Assert.That(new List<string>
            {
                "Subp Yao"
            },Is.EqualTo( info.Artists));
            Assert.That(new List<string>
            {
                "Astronautalis"
            }, Is.EqualTo(info.Features));
            Assert.That(emptyStringList,Is.EqualTo( info.Remixers));
            Assert.That("Lost (Floating)", Is.EqualTo(info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Title3()
        {
            var info = TrackParser.GetTrackInfo("i_o & LIGHTS", "Annihilation (Afterhours Mix)", "i_o & LIGHTS", "Annihilation (Afterhours Mix)", DateTime.Now);
            Assert.That(new List<string>
            {
                "i_o",
                "LIGHTS"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList, Is.EqualTo(info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Annihilation (Afterhours Mix)",Is.EqualTo( info.ProcessedTitle)); //bug make this "Annihilation" + "Afterhours Mix" subtitle
        }

        [Test]
        public void TrackParser_Title4()
        {
            var info = TrackParser.GetTrackInfo("Ty Dolla $ign", "Ego Death (feat. Kanye West, FKA twigs, & Skrillex)", "", "", DateTime.Now);
            Assert.That(new List<string>
            {
                "Ty Dolla $ign"
            },Is.EqualTo( info.Artists));
            Assert.That(new List<string>
            {
                "Kanye West",
                "FKA twigs",
                "Skrillex"
            },Is.EqualTo( info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Ego Death",Is.EqualTo( info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Title5()
        {
            var info = TrackParser.GetTrackInfo("Monstergetdown", "maybe nothing (Extended Edit)", "Monstergetdown", "maybe nothing (Extended Edit)", DateTime.Now);
            Assert.That(new List<string>
            {
                "Monstergetdown"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList, Is.EqualTo(info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("maybe nothing (Extended Edit)", Is.EqualTo(info.ProcessedTitle)); //bug make this "maybe nothing" + "Extended Edit" subtitle
        }

        [Test]
        public void TrackParser_Title6()
        {
            var info = TrackParser.GetTrackInfo("BUDD", "Triangular (Radio Edit)", "BUDD", "Triangular", DateTime.Now);
            Assert.That(new List<string>
            {
                "BUDD"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList,Is.EqualTo( info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Triangular (Radio Edit)", Is.EqualTo(info.ProcessedTitle)); //bug make this "Triangular" + "Radio Edit" subtitle
        }

        [Test]
        public void TrackParser_Title7()
        {
            var info = TrackParser.GetTrackInfo("deadmau5", "Strobe (Club Edit)", "deadmau5", "Strobe", DateTime.Now);
            Assert.That(new List<string>
            {
                "deadmau5"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList,Is.EqualTo( info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Strobe (Club Edit)", Is.EqualTo(info.ProcessedTitle)); //bug make this "Strobe" + "Club Edit" subtitle
        }

        [Test]
        public void TrackParser_Remix()
        {
            var info = TrackParser.GetTrackInfo("Mystic State", "Ever More (War Remix)", "Mystic State", "Ever More & Close Thirteen", DateTime.Now);
            Assert.That(new List<string>
            {
                "Mystic State"
            },Is.EqualTo( info.Artists));
            Assert.That(emptyStringList,Is.EqualTo( info.Features));
            Assert.That(new List<string>
            {
                "War"
            },Is.EqualTo( info.Remixers));
            Assert.That("Ever More",Is.EqualTo( info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Remix2()
        {
            var info = TrackParser.GetTrackInfo("Zeds Dead x Loge21", "Just Wanna (sumthin sumthin Remix) [feat. Polina]", "Zeds Dead", "WE ARE DEADBEATS (Vol. 4/Deluxe)", DateTime.Now);
            Assert.That(new List<string>
            {
                "Zeds Dead",
                "Loge21"
            }, Is.EqualTo(info.Artists));
            Assert.That(new List<string>
            {
                "Polina"
            }, Is.EqualTo(info.Features));
            Assert.That(new List<string>
            {
                "sumthin sumthin"
            },Is.EqualTo( info.Remixers));
            Assert.That("Just Wanna", Is.EqualTo(info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Remix3()
        {
            var info = TrackParser.GetTrackInfo("deadmau5 & The Neptunes", "Pomegranate (Ninajirachi Extended Remix)", "deadmau5 & The Neptunes", "Pomegranate (Ninajirachi Extended Remix)", DateTime.Now);
            Assert.That(new List<string>
            {
                "deadmau5",
                "The Neptunes"
            },Is.EqualTo( info.Artists));
            Assert.That(emptyStringList, Is.EqualTo(info.Features));
            Assert.That(new List<string>
            {
                "Ninajirachi"
            }, Is.EqualTo(info.Remixers));
            Assert.That("Pomegranate",Is.EqualTo( info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Remix4()
        {
            var info = TrackParser.GetTrackInfo("Romare", "All Night (Karma Fields Edit)", "Romare", "All Night (Karma Fields Edit)", DateTime.Now);
            Assert.That(new List<string>
            {
                "Romare"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList, Is.EqualTo(info.Features));
            Assert.That(new List<string>
            {
                "Karma Fields"
            }, Is.EqualTo(info.Remixers));
            Assert.That("All Night",Is.EqualTo( info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Remix5()
        {
            var info = TrackParser.GetTrackInfo("Wingz", "I Got (Vorso 20:17 Remix)", "Vorso", "", DateTime.Now);
            Assert.That(new List<string>
            {
                "Wingz"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList, Is.EqualTo(info.Features));
            Assert.That(new List<string>
            {
                "Vorso"
            }, Is.EqualTo(info.Remixers));
            Assert.That("I Got", Is.EqualTo(info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Artist()
        {
            var info = TrackParser.GetTrackInfo("MOGUAI x Rebecca & Fiona", "Sad Boy, Happy Girl", "MOGUAI x Rebecca & Fiona", "Sad Boy, Happy Girl", DateTime.Now);
            Assert.That(new List<string>
            {
                "MOGUAI",
                "Rebecca & Fiona"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList,Is.EqualTo( info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Sad Boy, Happy Girl", Is.EqualTo(info.ProcessedTitle));
        }

        [Test]
        public void TrackParser_Artist2()
        {
            var info = TrackParser.GetTrackInfo("MOGUAI & Rebecca & Fiona", "Sad Boy, Happy Girl", "MOGUAI & Rebecca & Fiona", "Sad Boy, Happy Girl", DateTime.Now);
            Assert.That(new List<string>
            {
                "MOGUAI",
                "Rebecca & Fiona"
            }, Is.EqualTo(info.Artists));
            Assert.That(emptyStringList, Is.EqualTo(info.Features));
            Assert.That(emptyStringList, Is.EqualTo(info.Remixers));
            Assert.That("Sad Boy, Happy Girl",Is.EqualTo( info.ProcessedTitle));
        }
    }
}