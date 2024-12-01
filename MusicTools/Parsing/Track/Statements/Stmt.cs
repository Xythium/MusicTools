namespace MusicTools.Parsing.Track.Statements;

public abstract class Stmt
{
    public abstract string Accept(IStmtVisitor visitor);
}