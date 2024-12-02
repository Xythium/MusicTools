using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MusicTools.Parsing.Track;
using Newtonsoft.Json;
using NUnit.Framework;
using VerifyNUnit;
using VerifyTests;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MusicToolTests;

[TestFixture]
public class TrackParserTests
{
    private VerifySettings settings;

    [OneTimeSetUp]
    public void Setup()
    {
        settings = new VerifySettings();
        settings.UseDirectory("snapshots");

        Token.Keywords.GetType();
    }

    [Test, TestCaseSource(typeof(TrackTests), nameof(TrackTests.TestCases))]
    public Task Track(string line)
    {
        var scanner = new Scanner(line);
        var tokens = scanner.ScanTokens().ToArray();

        var parser = new Parser();
        var stmts = parser.Parse(tokens);

        //Console.WriteLine(JsonConvert.SerializeObject(stmts, Formatting.Indented));

        var reconstructor = new Reconstructor();
        var reconstructed = reconstructor.Reconstruct(stmts);

        Console.WriteLine(JsonConvert.SerializeObject(reconstructor.TrackInfo, Formatting.Indented));

        return Verifier.Verify(reconstructor.TrackInfo, settings);
    }
}

public class TrackTests
{
    public static IEnumerable TestCases
    {
        get
        {
            yield return new TestCaseData("DAWN (feat. Totally Enormous Extinct Dinosaur) (Tunnelvisions Remix)");
            yield return new TestCaseData("Follow You (Fractal Chill Mix) [feat. Danyka Nadeau]");
            yield return new TestCaseData("Departed (Boy Kid Cloud VIP)");
            yield return new TestCaseData("Hold Your Breath (Vorso Instrumental Mix)");
            yield return new TestCaseData("Somewhere in Between (feat. Colleen D'Agostino) [Extended Mix]");
            yield return new TestCaseData("Limbic (Value of Stimuli)");
            yield return new TestCaseData("Lost (Floating) (feat. Astronautalis)");
            yield return new TestCaseData("Strobe (Club Edit)");
            yield return new TestCaseData("Pomegranate (Ninajirachi Extended Remix)");
            yield return new TestCaseData("I Got (Vorso 20:17 Remix)");
            yield return new TestCaseData("If There Ever Comes a Day (feat. Eli \"Paperboy\" Reed & Louis Futon [IHF Remix]");
            yield return new TestCaseData("Remind Me (Someone Else's Radio Remix");
            yield return new TestCaseData("Cracks (feat. Belle Humble) [Demus Radio Edit]");
        }
    }
}