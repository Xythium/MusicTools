using MusicTools.Parsing.Track.Expressions;
using MusicTools.Parsing.Track.Statements;

namespace MusicTools.Parsing.Track;

public interface IExprVisitor
{
    string VisitFeat(FeatExpr feat);

    string VisitGrouping(GroupingExpr grouping);

    string VisitName(NameExpr name);

    string VisitRemix(RemixExpr remix);

    string VisitVariable(VariableExpr variable);
}

public interface IStmtVisitor
{
    string VisitExpr(ExprStmt expr);

    string VisitFeat(FeatStmt feat);
}