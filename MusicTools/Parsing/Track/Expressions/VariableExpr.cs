namespace MusicTools.Parsing.Track.Expressions;

public class VariableExpr : Expr
{
    public required Token Name { get; init; }

    public override string Accept(IExprVisitor visitor)
    {
        return visitor.VisitVariable(this);
    }
}