using System;

namespace MusicTools.Parsing.Track.Expressions;

public class RemixExpr : Expr
{
    public required Expr Artist { get; set; }

    public required string Type { get; set; }

    public override string Accept(IExprVisitor visitor)
    {
        return visitor.VisitRemix(this);
    }
}