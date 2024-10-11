using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicTools.Parsing.Subgenre;

public class SubgenreParser
{
    public SubgenreParser() { }

    public void Parse(string input)
    {
        var strings = new List<string>();
        var index = 0;

        var word = new StringBuilder();

        while (index < input.Length)
        {
            var current = input[index];
                

            if (index == input.Length - 1)
            {
                word.Append(current);
                strings.Add(word.ToString().Trim());
            }
            else
            {
                var next = input[index + 1];

                checkSeparator('|', current, strings, word);
                checkSeparator('>', current, strings, word);
                checkSeparator('~', current, strings, word);
                    
                word.Append(current);
            }

            index++;
        }

        Console.WriteLine(string.Join("\r\n", strings.Select(s => $"'{s}'")));
        var tokens = new List<Token>();
    }

    private static void checkSeparator(char separator, char toCheck, List<string> strings, StringBuilder word)
    {
        if (toCheck == separator)
        {
            //word.Append(toCheck);
            strings.Add(word.ToString().Trim());
            word.Clear();
        }
    }
}

public class Token
{
}