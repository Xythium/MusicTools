using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MusicTools.Parsing.Subgenre;
using MusicTools.Utils;
using NUnit.Framework;

namespace MusicToolTests;

public class GenreParserTests
{
    [Test]
    public void Subgenre()
    {
        var lines = File.ReadAllLines("subgenres.txt");

        foreach (var line in lines)
        {
            if (line == "-")
                continue;
           
            try
            {
                var scanner = new Scanner(line);
                var tokens = scanner.ScanTokens();

                var parser = new Parser(tokens);
                var expr = parser.Parse();
            }
            catch (ParseError e)
            {
                ;
            }
            catch (Exception e)
            {
                ;
            }

        }

    }
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