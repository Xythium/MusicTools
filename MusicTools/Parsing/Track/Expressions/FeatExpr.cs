namespace MusicTools.Parsing.Track.Expressions;

public class FeatExpr : Expr
{
    public required Token Operator { get; set; }

    public required Expr Artist { get; set; }

    public override string Accept(IExprVisitor visitor)
    {
 return visitor.VisitFeat(this);
    }
}