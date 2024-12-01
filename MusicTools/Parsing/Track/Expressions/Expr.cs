namespace MusicTools.Parsing.Track.Expressions;

public abstract class Expr
{
    public abstract string Accept(IExprVisitor visitor);
}