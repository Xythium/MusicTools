using System;
using MusicTools.Parsing.Subgenre;
using MusicTools.Utils;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /* var parser = new SubgenreParser();
 
             parser.Parse("Bass House > Tech Trance");*/

            Console.WriteLine(JsonConvert.SerializeObject(SubgenresUtils.Parse("Bass House > Tech Trance"), Formatting.Indented));
            ;
        }
    }
}