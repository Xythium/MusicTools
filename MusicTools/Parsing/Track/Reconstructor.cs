using System.Collections.Generic;
using System.Text;
using MusicTools.Parsing.Track.Expressions;
using MusicTools.Parsing.Track.Statements;

namespace MusicTools.Parsing.Track;

public class Reconstructor : IExprVisitor, IStmtVisitor
{
    public string FromString(string str)
    {
        var scanner = new Scanner(str);
        return FromScanner(scanner);
    }

    public string FromScanner(Scanner scanner)
    {
        var tokens = scanner.ScanTokens();
        var parser = new Parser(tokens);
        return FromParser(parser);
    }

    public string FromParser(Parser parser)
    {
        var stmts = parser.Parse();
        var reconstructed = Reconstruct(stmts);
        return reconstructed;
    }

    public string Reconstruct(List<Stmt> statements)
    {
        var sb = new StringBuilder();
        foreach (var stmt in statements)
        {
            sb.Append(stmt.Accept(this));
            sb.Append(' ');
        }

        return sb.ToString().Trim();
    }

    public string VisitExpr(ExprStmt expr)
    {
        return expr.Expression.Accept(this);
    }

    public string VisitFeat(FeatStmt feat)
    {
        return "";
    }

    public string VisitFeat(FeatExpr feat)
    {
        var name = feat.Artist.Accept(this);
        return $"feat. {name}";
    }

    public string VisitGrouping(GroupingExpr grouping)
    {
        return $"({grouping.Group.Accept(this)})";
    }

    public string VisitName(NameExpr name)
    {
        return name.Name;
    }

    public string VisitRemix(RemixExpr remix)
    {
        return $"{remix.Artist.Accept(this)} {remix.Type}";
    }

    public string VisitVariable(VariableExpr variable)
    {
        return "var";
    }
}