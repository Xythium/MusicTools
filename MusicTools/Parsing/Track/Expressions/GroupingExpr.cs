namespace MusicTools.Parsing.Track.Expressions;

public class GroupingExpr : Expr
{
    public required Expr Group { get; init; }

    public override string Accept(IExprVisitor visitor)
    {
        return visitor.VisitGrouping(this);
    }
}