namespace MusicTools.Parsing.Track.Expressions;

public class NameExpr : Expr
{
    public required string Name { get; set; }
    
    public override string Accept(IExprVisitor visitor)
    {
        return visitor.VisitName(this);
    }
   
}