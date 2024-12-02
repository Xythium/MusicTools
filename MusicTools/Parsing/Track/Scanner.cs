using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MusicTools.Parsing.Track;

public class Scanner
{
    private readonly List<Token> tokens;
#if NET5_0_OR_GREATER
    private readonly ReadOnlyMemory<char> sourceSpan;
    private int _length;
#else
    private string source;
#endif

    private int _start = 0;
    private int _current = 0;
    private int _line = 1;
    private int _position = 0;


    public Scanner(string source)
    {
#if NET5_0_OR_GREATER
        sourceSpan = source.AsMemory();
        _length = sourceSpan.Length;
#else
        this.source = source;
#endif
        tokens = new List<Token>(16);
    }

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            ScanToken();
        }

        tokens.Add(new Token
        {
            Type = TokenType.Eof,
            Lexeme = "",
            Literal = "",
            Line = _line,
            Position = _position
        });
        return tokens;
    }

    private void ScanToken()
    {
        var c = Advance();
        ;
        switch (c)
        {
            case '(': AddToken(TokenType.LeftParen); break;

            case ')': AddToken(TokenType.RightParen); break;

            case '[': AddToken(TokenType.LeftBrace); break;

            case ']': AddToken(TokenType.RightBrace); break;

            case '\n':
                _line++;
                _position = 0;
                break;

            default:
                if (IsAlphaNumeric(c))
                    Identifier();
                else if (c == ' ' || c == '\u00a0')
                    break;
                else
                    Error(_line, _position, $"Unexpected character: '{c}'.");
                break;
        }
    }

    private void Identifier()
    {
        while (IsName(Peek()))
            Advance();

#if NET5_0_OR_GREATER
        var text = sourceSpan.Slice(_start, _position - _start).ToString();
#else
        var text = source.Substring(_start, _current - _start);
#endif
        if (!Token.Keywords.TryGetValue(text, out var tokenType))
        {
            if (text == "Chill")
            {
                Advance();
                Identifier();
                return;
            }

            tokenType = TokenType.Identifier;
        }

        AddToken(tokenType);
    }

    /*private bool Match(char expected)
    {
        if (IsAtEnd())
            return false;

        if (source[_current] != expected)
            return false;

        _current++;
        return true;
    }*/

    private char Peek()
    {
        if (IsAtEnd())
            return '\0';

#if NET5_0_OR_GREATER
        return sourceSpan.Span[_current];
        //return sourceSpan.Slice(_start, _position - _start).Span[0];
#else
        return source[_current];
#endif
    }

    /*private char PeekNext()
    {
        if (_current + 1 >= source.Length)
            return '\0';

        return source[_current + 1];
    }*/

    /*private char Previous()
    {
        if (_current == 0)
            return '\0';

        return source[_current - 1];
    }*/

    private static bool IsName(char c)
    {
        return IsAlphaNumeric(c) || (c != ' ' && c != '\0' && c != '(' && c != ')' && c != '[' && c != ']'); //'\u00a0' or '-' or '_' or '&' or '.' or '/' or '\'' or '"' or ':' or ','; // todo: make [] a class
    }

    private static bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    private static bool IsAlpha(char c)
    {
        return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or 'è' or 'é' or 'ó' or 'ä' or 'ã' or 'ă' or 'ç';
    }

    private static bool IsAlphaNumeric(char c)
    {
        return IsAlpha(c) || IsDigit(c) || c == '"' || c == '&';
    }

    private bool IsAtEnd()
    {
#if NET5_0_OR_GREATER
        return _current >= _length;
#else
        return _current >= source.Length;
#endif
    }

    private char Advance()
    {
        _position++;
#if NET5_0_OR_GREATER
        return sourceSpan.Span[_current++];
#else
        return source[_current++];
#endif
    }

    private void AddToken(TokenType type, string literal = "")
    {
#if NET5_0_OR_GREATER
        var text = sourceSpan.Slice(_start, _current - _start).ToString();
        //var span = source.AsSpan(_start, _current - _start);
        //var text = span.ToString();
#else
        var text = source.Substring(_start, _current - _start);
#endif
        tokens.Add(new Token
        {
            Type = type,
            Lexeme = text,
            Literal = literal,
            Line = _line,
            Position = _position
        });
    }

    internal static void Error(int line, int position, string message)
    {
        Report(line, position, "", message);
    }

    internal static void Error(Token token, string message)
    {
        if (token.Type == TokenType.Eof)
        {
            Report(token.Line, token.Position, " at end", message);
        }
        else
        {
            Report(token.Line, token.Position, $" at '{token.Lexeme}'", message);
        }
    }

    internal static void Report(int line, int position, string where, string message)
    {
        Console.Error.WriteLine($"[line {line}:{position}] Error{where}: {message}");
        hadError = true;
    }

    internal static bool hadError = false;
}