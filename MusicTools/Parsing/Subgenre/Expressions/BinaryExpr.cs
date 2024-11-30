namespace MusicTools.Parsing.Subgenre.Expressions;

public class BinaryExpr : Expr
{
    public int Id { get; private set; }

    public required Expr Left { get; init; }

    public required Token Operator { get; init; }

    public required Expr Right { get; init; }

    private static int counter = 1;

    public BinaryExpr()
    {
        Id = counter++;
    }

    public override string Accept(Visitor visitor)
    {
        return visitor.VisitBinary(this);
    }
}