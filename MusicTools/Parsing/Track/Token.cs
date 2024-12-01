using System;
#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicTools.Parsing.Track;

public struct Token
{
    public required TokenType Type { get; init; }

    public required string Lexeme { get; init; }

    public required string Literal { get; init; }

    public required int Line { get; init; }

    public required int Position { get; init; }

    public override string ToString()
    {
        return $"{Type} {Lexeme} {Literal}";
    }

    private static readonly Dictionary<string, TokenType> keywords = new(StringComparer.InvariantCultureIgnoreCase)
    {
        {
            "featuring", TokenType.Featuring
        },
        {
            "feat.", TokenType.FeatDot
        },
        {
            "feat", TokenType.Feat
        },
        {
            "ft.", TokenType.FtDot
        },
        {
            "ft", TokenType.Ft
        },
        {
            "Remix", TokenType.Remix
        },
        {
            "VIP", TokenType.Vip
        },
        {
            "Chill Mix", TokenType.ChillMix
        }
    };

#if NET8_0_OR_GREATER // frozen maybe if gets large
    public static readonly ReadOnlyDictionary<string, TokenType> Keywords = new(keywords);
#else
    public static readonly ReadOnlyDictionary<string, TokenType> Keywords = new(keywords);
#endif
}