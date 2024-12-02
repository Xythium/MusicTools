using System;
using System.Collections.Generic;
using MusicTools.Parsing.Track.Expressions;
using MusicTools.Parsing.Track.Statements;

namespace MusicTools.Parsing.Track;

public class Parser
{
#if NET5_0_OR_GREATER
#else
    //private Token[] tokens;
#endif

    private int current = 0;

#if NET5_0_OR_GREATER
#else
    public Parser(Token[] tokens)
    {
        //this.tokens = tokens;
    }
#endif

#if NET5_0_OR_GREATER
    public List<Stmt> Parse(ReadOnlySpan<Token> tokens)
#else
    public List<Stmt> Parse(Token[] tokens)
#endif
    {
        var stmts = new List<Stmt>(tokens.Length);
        try
        {
            while (!IsAtEnd(tokens))
            {
                var stmt = Statement(tokens);
                stmts.Add(stmt);
            }
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

        return stmts;
    }

#if NET5_0_OR_GREATER
    private Stmt Statement(ReadOnlySpan<Token> tokens)
#else
    private Stmt Statement(Token[] tokens)
#endif
    {
        var expr = Expression(tokens);
        return new ExprStmt
        {
            Expression = expr,
        };
    }

#if NET5_0_OR_GREATER
    private Expr Expression(ReadOnlySpan<Token> tokens)
#else
    private Expr Expression(Token[] tokens)
#endif
    {
        if (Match(tokens, TokenType.Featuring, TokenType.FeatDot, TokenType.Feat, TokenType.FtDot, TokenType.Ft))
        {
            return new FeatExpr
            {
                Operator = Previous(tokens),
                Artist = Expression(tokens),
            };
        }

        if (Match(tokens, TokenType.Identifier))
        {
            var start = Previous(tokens);
            var text = start.Lexeme;

            while (Check(tokens, TokenType.Identifier))
            {
                Advance(tokens);
                text += $" {Previous(tokens).Lexeme}";
            }

            if (Match(tokens, TokenType.Remix, TokenType.Vip, TokenType.ChillMix))
            {
                return new RemixExpr
                {
                    Artist = new NameExpr
                    {
                        Name = text
                    },
                    Type = Previous(tokens).Lexeme,
                };
            }

            return new NameExpr
            {
                Name = text
            };
        }

        if (Match(tokens, TokenType.LeftParen))
        {
            var expr = Expression(tokens);

            if (Peek(tokens).Type == TokenType.LeftBrace)
            {
                return new GroupingExpr
                {
                    Group = expr
                };
            }

            if (Peek(tokens).Type == TokenType.Eof)
            {
                return new GroupingExpr
                {
                    Group = expr
                };
            }

            Consume(tokens, "Expect ')' after expression", TokenType.RightParen);
            return new GroupingExpr
            {
                Group = expr
            };
        }

        if (Match(tokens, TokenType.LeftBrace))
        {
            var expr = Expression(tokens);
            Consume(tokens, "Expect '}' after expression", TokenType.RightBrace);
            return new GroupingExpr
            {
                Group = expr
            };
        }

        throw Error(Peek(tokens), "Expect expression");
    }

    private Expr Term()
    {
        var expr = Primary();

        return expr;
    }

    private Expr Primary()
    {
        return null;
    }

#if NET5_0_OR_GREATER
    private bool Match(ReadOnlySpan<Token> tokens, params TokenType[] types)
#else
    private bool Match(Token[] tokens, params TokenType[] types)
#endif
    {
        foreach (var type in types)
        {
            if (Check(tokens, type))
            {
                Advance(tokens);
                return true;
            }
        }

        return false;
    }

#if NET5_0_OR_GREATER
    private Token Consume(ReadOnlySpan<Token> tokens, string message, params TokenType[] types)
#else
    private Token Consume(Token[] tokens, string message, params TokenType[] types)
#endif
    {
        foreach (var type in types)
        {
            if (Check(tokens, type))
            {
                return Advance(tokens);
            }
        }

        throw Error(Peek(tokens), message);
    }

#if NET5_0_OR_GREATER
    private bool Check(ReadOnlySpan<Token> tokens, TokenType type)
#else
    private bool Check(Token[] tokens, TokenType type)
#endif
    {
        if (IsAtEnd(tokens))
            return false;

        return Peek(tokens).Type == type;
    }

#if NET5_0_OR_GREATER
    private Token Advance(ReadOnlySpan<Token> tokens)
#else
    private Token Advance(Token[] tokens)
#endif
    {
        if (!IsAtEnd(tokens))
            current++;

        return Previous(tokens);
    }

#if NET5_0_OR_GREATER
    private bool IsAtEnd(ReadOnlySpan<Token> tokens)
#else
    private bool IsAtEnd(Token[] tokens)
#endif
    {
        return current+1 >= tokens.Length;
        //return Peek(tokens).Type == TokenType.Eof;
    }

#if NET5_0_OR_GREATER
    private Token Peek(ReadOnlySpan<Token> tokens)
#else
    private Token Peek(Token[] tokens)
#endif
    {
#if NET5_0_OR_GREATER
        return tokens[current];
#else
        return tokens[current];
#endif
    }

#if NET5_0_OR_GREATER
    private Token Previous(ReadOnlySpan<Token> tokens)
#else
    private Token Previous(Token[] tokens)
#endif
    {
#if NET5_0_OR_GREATER
        return tokens[current - 1];
#else
        return tokens[current - 1];
#endif
    }

    private ParseError Error(Token token, string message)
    {
        Scanner.Error(token, message);
        return new ParseError(message);
    }
}