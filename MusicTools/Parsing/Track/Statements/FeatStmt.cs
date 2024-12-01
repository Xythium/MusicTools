namespace MusicTools.Parsing.Track.Statements;

public class FeatStmt : Stmt
{
    public override string Accept(IStmtVisitor visitor)
    {
      return visitor.VisitFeat(this);
    }
}