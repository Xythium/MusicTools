using System.Collections;
using System.Collections.Generic;
using MusicTools.Parsing.Subgenre;
using MusicTools.Utils;
using NUnit.Framework;

namespace MusicToolTests;

public class GenreParserTests
{
    private SubgenreParser parser;

    [SetUp]
    public void Setup() { parser = new SubgenreParser(); }

    [TestCaseSource(typeof(Data), nameof(Data.Cases))]
    public List<string> Subgenre(string input) { return SubgenresUtils.SplitSubgenres(input); }
}

public class Data
{
    public static IEnumerable Cases
    {
        get
        {
            yield return new TestCaseData("Melodic Dubstep ~ Brostep").Returns(new List<string>
            {
                "Melodic Dubstep",
                "Brostep"
            });
        }
    }
}