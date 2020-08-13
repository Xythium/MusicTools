using System;
using System.Collections.Generic;
using MusicTools.Parsing.Track;
using NUnit.Framework;

namespace MusicToolTests
{
    public class Tests
    {
        [SetUp]
        public void Setup() { }

        private readonly List<string> emptyStringList = new List<string>();

        [Test]
        public void TrackParser_Extended_Mix()
        {
            var info = TrackParser.GetTrackInfo("EDDIE", "Somewhere in Between (feat. Colleen D'Agostino) [Extended Mix]", "EDDIE", "Somewhere in Between", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "EDDIE"
            }, info.Artists);
            Assert.AreEqual(new List<string>
            {
                "Colleen D'Agostino"
            }, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Somewhere in Between", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Extended_Mix2()
        {
            var info = TrackParser.GetTrackInfo("EDDIE", "Somewhere in Between (feat. Colleen D'Agostino) (Extended Mix)", "EDDIE", "Somewhere in Between", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "EDDIE"
            }, info.Artists);
            Assert.AreEqual(new List<string>
            {
                "Colleen D'Agostino"
            }, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Somewhere in Between", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Title()
        {
            var info = TrackParser.GetTrackInfo("yunis", "Limbic (Value of Stimuli)", "yunis", "Amygdala", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "yunis"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Limbic (Value of Stimuli)", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Title2()
        {
            var info = TrackParser.GetTrackInfo("Subp Yao", "Lost (Floating) (feat. Astronautalis)", "Subp Yao", "Infra Aqual", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "Subp Yao"
            }, info.Artists);
            Assert.AreEqual(new List<string>
            {
                "Astronautalis"
            }, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Lost (Floating)", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Title3()
        {
            var info = TrackParser.GetTrackInfo("i_o & LIGHTS", "Annihilation (Afterhours Mix)", "i_o & LIGHTS", "Annihilation (Afterhours Mix)", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "i_o",
                "LIGHTS"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Annihilation (Afterhours Mix)", info.ProcessedTitle); //bug make this "Annihilation" + "Afterhours Mix" subtitle
        }

        [Test]
        public void TrackParser_Title4()
        {
            var info = TrackParser.GetTrackInfo("Ty Dolla $ign", "Ego Death (feat. Kanye West, FKA twigs, & Skrillex)", "", "", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "Ty Dolla $ign"
            }, info.Artists);
            Assert.AreEqual(new List<string>
            {
                "Kanye West",
                "FKA twigs",
                "Skrillex"
            }, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Ego Death", info.ProcessedTitle);
        }
        
        [Test]
        public void TrackParser_Title5()
        {	
            var info = TrackParser.GetTrackInfo("Monstergetdown", "maybe nothing (Extended Edit)", "Monstergetdown", "maybe nothing (Extended Edit)", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "Monstergetdown"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("maybe nothing (Extended Edit)", info.ProcessedTitle); //bug make this "maybe nothing" + "Extended Edit" subtitle
        }

        [Test]
        public void TrackParser_Title6()
        {		
            var info = TrackParser.GetTrackInfo("BUDD", "Triangular (Radio Edit)", "BUDD", "Triangular", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "BUDD"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Triangular (Radio Edit)", info.ProcessedTitle); //bug make this "Triangular" + "Radio Edit" subtitle
        }
        
        [Test]
        public void TrackParser_Title7()
        {			
            var info = TrackParser.GetTrackInfo("deadmau5", "Strobe (Club Edit)", "deadmau5", "Strobe", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "deadmau5"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Strobe (Club Edit)", info.ProcessedTitle); //bug make this "Strobe" + "Club Edit" subtitle
        }

        [Test]
        public void TrackParser_Remix()
        {
            var info = TrackParser.GetTrackInfo("Mystic State", "Ever More (War Remix)", "Mystic State", "Ever More & Close Thirteen", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "Mystic State"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(new List<string>
            {
                "War"
            }, info.Remixers);
            Assert.AreEqual("Ever More", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Remix2()
        {
            var info = TrackParser.GetTrackInfo("Zeds Dead x Loge21", "Just Wanna (sumthin sumthin Remix) [feat. Polina]", "Zeds Dead", "WE ARE DEADBEATS (Vol. 4/Deluxe)", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "Zeds Dead",
                "Loge21"
            }, info.Artists);
            Assert.AreEqual(new List<string>
            {
                "Polina"
            }, info.Features);
            Assert.AreEqual(new List<string>
            {
                "sumthin sumthin"
            }, info.Remixers);
            Assert.AreEqual("Just Wanna", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Remix3()
        {
            var info = TrackParser.GetTrackInfo("deadmau5 & The Neptunes", "Pomegranate (Ninajirachi Extended Remix)", "deadmau5 & The Neptunes", "Pomegranate (Ninajirachi Extended Remix)", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "deadmau5",
                "The Neptunes"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(new List<string>
            {
                "Ninajirachi"
            }, info.Remixers);
            Assert.AreEqual("Pomegranate", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Remix4()
        {
            var info = TrackParser.GetTrackInfo("Romare", "All Night (Karma Fields Edit)", "Romare", "All Night (Karma Fields Edit)", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "Romare"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(new List<string>
            {
                "Karma Fields"
            }, info.Remixers);
            Assert.AreEqual("All Night", info.ProcessedTitle);
        }
        
        [Test]
        public void TrackParser_Remix5()
        {	
            var info = TrackParser.GetTrackInfo("Wingz", "I Got (Vorso 20:17 Remix)", "Vorso", "", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "Wingz"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(new List<string>
            {
                "Vorso"
            }, info.Remixers);
            Assert.AreEqual("I Got", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Artist()
        {
            var info = TrackParser.GetTrackInfo("MOGUAI x Rebecca & Fiona", "Sad Boy, Happy Girl", "MOGUAI x Rebecca & Fiona", "Sad Boy, Happy Girl", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "MOGUAI",
                "Rebecca & Fiona"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Sad Boy, Happy Girl", info.ProcessedTitle);
        }

        [Test]
        public void TrackParser_Artist2()
        {
            var info = TrackParser.GetTrackInfo("MOGUAI & Rebecca & Fiona", "Sad Boy, Happy Girl", "MOGUAI & Rebecca & Fiona", "Sad Boy, Happy Girl", DateTime.Now);
            Assert.AreEqual(new List<string>
            {
                "MOGUAI",
                "Rebecca & Fiona"
            }, info.Artists);
            Assert.AreEqual(emptyStringList, info.Features);
            Assert.AreEqual(emptyStringList, info.Remixers);
            Assert.AreEqual("Sad Boy, Happy Girl", info.ProcessedTitle);
        }
    }
}