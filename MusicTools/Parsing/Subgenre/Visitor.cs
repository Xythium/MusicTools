using MusicTools.Parsing.Subgenre.Expressions;

namespace MusicTools.Parsing.Subgenre;

public interface IVisitor
{
    string VisitBinary(BinaryExpr binary);
    string VisitGrouping(GroupingExpr grouping);
    string VisitVariable(VariableExpr variable);
}