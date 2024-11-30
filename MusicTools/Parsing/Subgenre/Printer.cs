using System.ComponentModel;
using System.Text;
using MusicTools.Parsing.Subgenre.Expressions;

namespace MusicTools.Parsing.Subgenre;

public class Printer : Visitor
{
    public string Print(Expr expr)
    {
        return expr.Accept(this);
    }

    public string VisitBinary(BinaryExpr binary)
    {
        return Parenthesize(binary.Operator.Lexeme, binary.Left, binary.Right);
    }


    public string VisitGrouping(GroupingExpr grouping)
    {
        return Parenthesize("group", grouping.Expression);
    }

    public string VisitVariable(VariableExpr variable)
    {
        return $"'{variable.Name.Lexeme}'";
    }

    private string Parenthesize(string name, params Expr[] exprs)
    {
        var sb = new StringBuilder();
        sb.Append("(").Append(name);

        foreach (var expr in exprs)
        {
            sb.Append(" ");
            sb.Append(expr.Accept(this));
        }

        sb.Append(")");
        return sb.ToString();
    }
}