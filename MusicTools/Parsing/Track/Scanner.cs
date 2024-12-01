using System;
using System.Collections.Generic;

namespace MusicTools.Parsing.Track;

public class Scanner(string source)
{
    private readonly List<Token> tokens = [];
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;
    private int _position = 0;

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

        var text = source.Substring(_start, _current - _start);
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

    private bool Match(char expected)
    {
        if (IsAtEnd())
            return false;

        if (source[_current] != expected)
            return false;

        _current++;
        return true;
    }

    private char Peek()
    {
        if (IsAtEnd())
            return '\0';

        return source[_current];
    }

    private char PeekNext()
    {
        if (_current + 1 >= source.Length)
            return '\0';

        return source[_current + 1];
    }

    private char Previous()
    {
        if (_current == 0)
            return '\0';

        return source[_current - 1];
    }

    private bool IsName(char c)
    {
        return IsAlphaNumeric(c) || (c != ' ' && c != '\0' && c!= '(' && c != ')' && c != '[' && c != ']'); //'\u00a0' or '-' or '_' or '&' or '.' or '/' or '\'' or '"' or ':' or ','; // todo: make [] a class
    }

    private bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    private bool IsAlpha(char c)
    {
        return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or 'è' or 'é' or 'ó' or 'ä' or 'ã' or 'ă' or 'ç';
    }

    private bool IsAlphaNumeric(char c)
    {
        return IsAlpha(c) || IsDigit(c) || c == '"' || c == '&';
    }

    private bool IsAtEnd()
    {
        return _current >= source.Length;
    }

    private char Advance()
    {
        _position++;
        return source[_current++];
    }

    private void AddToken(TokenType type)
    {
        AddToken(type, "");
    }

    private void AddToken(TokenType type, string literal)
    {
        var text = source.Substring(_start, _current - _start);
        tokens.Add(new Token
        {
            Type = type,
            Lexeme = text.Trim(),
            Literal = literal.Trim(),
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