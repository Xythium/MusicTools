namespace MusicTools.Parsing.Subgenre.Expressions;

public class GroupingExpr : Expr
{
    public required Expr Expression { get; init; }

    public override string Accept(Visitor visitor)
    {
        return visitor.VisitGrouping(this);
    }
}