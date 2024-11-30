namespace MusicTools.Parsing.Subgenre.Expressions;

public class VariableExpr : Expr
{
    public required Token Name { get; init; }

    public override string Accept(Visitor visitor)
    {
        return visitor.VisitVariable(this);
    }
}