using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using MusicTools.Parsing.Track;
using MusicTools.Parsing.Track.Statements;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Bench>();

            /*for (var i = 0; i < 1000; i++)
            {
                var reconstructor = new Reconstructor();
                var reconstructed = reconstructor.FromString("Ego Death (feat. Kanye West, FKA twigs, & Skrillex)");
            }*/


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
[MemoryDiagnoser(false)]
public class Bench
{
    [Benchmark(Baseline = true)]
    public TrackInfo SubstringWithRegex()
    {
        return TrackParser.GetTrackInfo("Ty Dolla $ign", "Ego Death (feat. Kanye West, FKA twigs, & Skrillex)", "", "", DateTime.Now);
    }

    [Benchmark]
    public List<Stmt> Tokenizer()
    {
        var scanner = new Scanner("Ego Death (feat. Kanye West, FKA twigs, & Skrillex)");
        var tokens = scanner.ScanTokens().ToArray();

        #if NET5_0_OR_GREATER
        var parser = new Parser();
        #else
        var parser = new Parser(tokens);
        #endif
        var stmts = parser.Parse(tokens);
        return stmts;
    }
}