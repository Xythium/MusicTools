using System;
using System.IO;
using MusicTools.Parsing.Subgenre;
using MusicTools.Parsing.Subgenre.Expressions;
using MusicTools.Utils;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = "Ambient > (Downtempo + Breakbeat) > Breakbeat Hardcore";

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
            File.WriteAllBytes("graph.png", graph);
        }
    }
}