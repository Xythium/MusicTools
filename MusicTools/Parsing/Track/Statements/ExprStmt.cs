using MusicTools.Parsing.Track.Expressions;

namespace MusicTools.Parsing.Track.Statements;

public class ExprStmt : Stmt
{
    public required Expr Expression { get; set; }
    
    public override string Accept(IStmtVisitor visitor)
    {
       return visitor.VisitExpr(this);
    }
}