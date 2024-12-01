using System;
using System.Collections.Generic;
using MusicTools.Parsing.Track.Expressions;
using MusicTools.Parsing.Track.Statements;

namespace MusicTools.Parsing.Track;

public class Parser(List<Token> tokens)
{
    private int current = 0;

    public List<Stmt> Parse()
    {
        var stmts = new List<Stmt>();
        try
        {
            while (!IsAtEnd())
            {
                var stmt = Statement();
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

    private Stmt Statement()
    {
        var expr = Expression();
        return new ExprStmt
        {
            Expression = expr,
        };
    }

    private Expr Expression()
    {
        if (Match(TokenType.Featuring, TokenType.FeatDot, TokenType.Feat, TokenType.FtDot, TokenType.Ft))
        {
            return new FeatExpr
            {
                Operator = Previous(),
                Artist = Expression(),
            };
        }

        if (Match(TokenType.Identifier))
        {
            var start = Previous();
            var text = start.Lexeme;

            while (Check(TokenType.Identifier))
            {
                Advance();
                text += $" {Previous().Lexeme}";
            }

            if (Match(TokenType.Remix, TokenType.Vip, TokenType.ChillMix))
            {
                return new RemixExpr
                {
                    Artist = new NameExpr
                    {
                        Name = text
                    },
                    Type = Previous().Lexeme,
                };
            }

            return new NameExpr
            {
                Name = text
            };
        }

        if (Match(TokenType.LeftParen))
        {
            var expr = Expression();
            
            if (Peek().Type == TokenType.LeftBrace)
            {
                return new GroupingExpr
                {
                    Group = expr
                };
            }

            if (Peek().Type == TokenType.Eof)
            {
                return new GroupingExpr
                {
                    Group = expr
                };
            }
            
            Consume("Expect ')' after expression", TokenType.RightParen);
            return new GroupingExpr
            {
                Group = expr
            };
        }

        if (Match(TokenType.LeftBrace))
        {
            var expr = Expression();
            Consume("Expect '}' after expression", TokenType.RightBrace);
            return new GroupingExpr
            {
                Group = expr
            };
        }

        throw Error(Peek(), "Expect expression");
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

    private Token Consume(string message, params TokenType[] types)
    {
        foreach (var type in types)
        {
            if (Check(type))
            {
                return Advance();
            }
        }

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