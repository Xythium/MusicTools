using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MusicTools.Parsing.Subgenre.Expressions;

namespace MusicTools.Parsing.Subgenre;

public class Parser(List<Token> tokens)
{
    private int current = 0;

    public Expr Parse()
    {
        try
        {
            return Expression();
        }
        catch (ParseError e)
        {
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private Expr Expression()
    {
        return Term();
    }

    private Expr Term()
    {
        var expr = Primary();

        while (Match(TokenType.Plus, TokenType.Greater, TokenType.Tilde, TokenType.PlusGreater, TokenType.GreaterPlus, TokenType.PlusTilde, TokenType.TildePlus))
        {
            var op = Previous();
            var right = Primary();
            expr = new BinaryExpr
            {
                Left = expr,
                Operator = op,
                Right = right
            };
        }

        return expr;
    }

    private Expr Primary()
    {
        if (Match(TokenType.Identifier))
        {
            return new VariableExpr
            {
                Name = Previous()
            };
        }

        if (Match(TokenType.LeftParen))
        {
            var expr = Expression();
            Consume(TokenType.RightParen, "Expect ')' after expression");
            return new GroupingExpr
            {
                Expression = expr
            };
        }

        throw Error(Peek(), "Expect expression");
    }

    private bool Match(params TokenType[] types)
    {
        foreach (var type in types)
        {
            if (Check(type))
            {
                Advance();
                return true;
            }
        }

        return false;
    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type))
            return Advance();

        throw Error(Peek(), message);
    }


    private bool Check(TokenType type)
    {
        if (IsAtEnd())
            return false;

        return Peek().Type == type;
    }

    private Token Advance()
    {
        if (!IsAtEnd())
            current++;

        return Previous();
    }

    private bool IsAtEnd()
    {
        return Peek().Type == TokenType.Eof;
    }

    private Token Peek()
    {
        return tokens[current];
    }

    private Token Previous()
    {
        return tokens[current - 1];
    }

    private ParseError Error(Token token, string message)
    {
        Scanner.Error(token, message);
        return new ParseError(message);
    }
}

public class ParseError(string message) : Exception(message);