using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicTools.Utils;

public static class SubgenresUtils
{
    private static readonly Regex subgenreRegex = new Regex("/ | > | \\| | \\+ | \\+~ | >\\+ | ~\\+ | \\+> | ~", RegexOptions.IgnoreCase);

    public static List<string> SplitSubgenres(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<string>();

        text = text.Replace("(", "")
            .Replace(")", "");

        var subgenres = new List<string>();
        var split = subgenreRegex.Split(text);

        for (var i = 0; i < split.Length; i++)
        {
            var subgenre = split[i];
            if (string.IsNullOrWhiteSpace(subgenre))
                continue;

            subgenre = subgenre.Trim();
            subgenres.Add(subgenre);
        }

        return subgenres;
    }

    public static List<string> Parse(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new List<string>();

        var result = text switch
        {
            [] => throw new Exception(),
            [..string left, '>', _] right => $"{left} | {right}",
            _ => "ooba dooba"
        };

        var subgenres = new List<string>
        {
            result
        };
        return subgenres;
    }
}