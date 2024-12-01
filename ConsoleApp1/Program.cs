using System;
using System.Collections.Generic;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using MusicTools.Parsing.Subgenre;
using MusicTools.Parsing.Subgenre.Expressions;
using MusicTools.Parsing.Track;
using MusicTools.Parsing.Track.Statements;
using MusicTools.Utils;
using Newtonsoft.Json;
using Parser = MusicTools.Parsing.Track.Parser;
using Scanner = MusicTools.Parsing.Track.Scanner;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<Bench>();

            var reconstructor = new Reconstructor();
            var reconstructed = reconstructor.FromString("Ego Death (feat. Kanye West, FKA twigs, & Skrillex)");


            /*var source = "Ambient > (Downtempo + Breakbeat) > Breakbeat Hardcore";

            var scanner = new Scanner(source);
            var tokens = scanner.ScanTokens();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }


            var parser = new Parser(tokens);
            var expr = parser.Parse();

            var printer = new Printer();
            Console.WriteLine(printer.Print(expr));

            var graph = new GraphImage().Graph(expr);
            File.WriteAllBytes("graph.png", graph);*/
        }
    }
}

[SimpleJob(RuntimeMoniker.Net481, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[DisassemblyDiagnoser]
[MemoryDiagnoser]
public class Bench
{
    [Benchmark(Baseline = true)]
    public TrackInfo Old()
    {
        return TrackParser.GetTrackInfo("Ty Dolla $ign", "Ego Death (feat. Kanye West, FKA twigs, & Skrillex)", "", "", DateTime.Now);
    }

    [Benchmark]
    public List<Stmt> New()
    {
        var scanner = new Scanner("Ego Death (feat. Kanye West, FKA twigs, & Skrillex)");
        var tokens = scanner.ScanTokens();

        var parser = new Parser(tokens);
        var stmts = parser.Parse();
        return stmts;
    }
}