using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using MusicTools.Parsing.Track;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MusicToolTests;

public class TrackParserTests
{
    [TestCaseSource(typeof(TrackTests), nameof(TrackTests.TestCases))]
    public string Track(string line)
    {
        var scanner = new Scanner(line);
        var tokens = scanner.ScanTokens();

        var parser = new Parser(tokens);
        var stmts = parser.Parse();

        Console.WriteLine(JsonConvert.SerializeObject(stmts, Formatting.Indented));

        var reconstructor = new Reconstructor();
        var reconstructed = reconstructor.Reconstruct(stmts);

        return reconstructed;
    }
}

public class TrackTests
{
    public static IEnumerable TestCases
    {
        get
        {
            yield return new TestCaseData("DAWN (feat. Totally Enormous Extinct Dinosaur) (Tunnelvisions Remix)").Returns("DAWN (feat. Totally Enormous Extinct Dinosaur) (Tunnelvisions Remix)");
            yield return new TestCaseData("Follow You (Fractal Chill Mix) [feat. Danyka Nadeau]").Returns("Follow You (feat. Danyka Nadeau) (Fractal Chill Mix)");
            yield return new TestCaseData("Departed (Boy Kid Cloud VIP)").Returns("Departed (Boy Kid Cloud VIP)");
            yield return new TestCaseData("Hold Your Breath (Vorso Instrumental Mix)").Returns("Hold Your Breath (Vorso Instrumental Mix)");
            yield return new TestCaseData("Somewhere in Between (feat. Colleen D'Agostino) [Extended Mix]").Returns("Somewhere in Between (feat. Colleen D'Agostino) (Extended Mix)");
            yield return new TestCaseData("Limbic (Value of Stimuli)").Returns("Limbic (Value of Stimuli)");
            yield return new TestCaseData("Lost (Floating) (feat. Astronautalis)").Returns("Lost (Floating) (feat. Astronautalis)");
            yield return new TestCaseData("Strobe (Club Edit)").Returns("Strobe (Club Edit)");
            yield return new TestCaseData("Pomegranate (Ninajirachi Extended Remix)").Returns("Pomegranate (Ninajirachi Extended Remix)");
            yield return new TestCaseData("I Got (Vorso 20:17 Remix)").Returns("I Got (Vorso 20:17 Remix)");
            yield return new TestCaseData("If There Ever Comes a Day (feat. Eli \"Paperboy\" Reed & Louis Futon [IHF Remix]").Returns("If There Ever Comes a Day (feat. Eli \"Paperboy\" Reed & Louis Futon) (IHF Remix)");
            yield return new TestCaseData("Remind Me (Someone Else's Radio Remix").Returns("Remind Me (Someone Else's Radio Remix)");
            yield return new TestCaseData("Cracks (feat. Belle Humble) [Demus Radio Edit]").Returns("Cracks (feat. Belle Humble) (Demus Radio Edit)");
        }
    }
}