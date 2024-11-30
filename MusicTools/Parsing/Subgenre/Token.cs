namespace MusicTools.Parsing.Subgenre;

public class Token
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
}