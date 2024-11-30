using System;
using System.Collections.Generic;
using System.Text;
using GraphVizNet;
using MusicTools.Parsing.Subgenre.Expressions;

namespace MusicTools.Parsing.Subgenre;

public class GraphImage : Visitor
{
    public byte[] Graph(Expr expr)
    {
        var graph = new GraphViz();

        var sb = new StringBuilder("graph G {\n");
        expr.Accept(this);

        foreach (var label in labels)
        {
            sb.AppendLine(label);
        }

        foreach (var (l, r) in edges)
        {
            sb.AppendLine($"{l} -- {r}");
        }

        sb.AppendLine("}");

        Console.WriteLine(sb.ToString());

        return graph.LayoutAndRender(null, sb.ToString(), null, null, "png");
    }

    private List<(string, string)> edges = new();
    private List<string> labels = new();

    public string VisitBinary(BinaryExpr binary)
    {
        var sb = new StringBuilder();

        labels.Add($"binary{binary.Id} [label=\"{binary.Operator.Lexeme}\"]");
        sb.AppendLine($"binary{binary.Id} [label=\"{binary.Operator.Lexeme}\"]");

        var left = binary.Left.Accept(this);
        if (binary.Left is BinaryExpr leftBinary)
        {
            edges.Add(($"binary{binary.Id}", $"binary{leftBinary.Id}"));
            //edges.Add(($"binary{leftBinary.Id}", "binary1"));
        }
        else if (binary.Left is GroupingExpr { Expression: BinaryExpr l })
        {
            edges.Add(($"binary{binary.Id}", $"binary{l.Id}"));
        }
        else if (!string.IsNullOrWhiteSpace(left) && left != "group")
        {
            edges.Add(($"binary{binary.Id}", left));
            //sb.AppendLine($"binary{binary.Id} -- {left}");
        }


        var right = binary.Right.Accept(this);
        if (binary.Right is BinaryExpr rightBinary)
        {
            edges.Add(($"binary{binary.Id}", $"binary{rightBinary.Id}"));
        }
        else if (binary.Right is GroupingExpr { Expression: BinaryExpr l })
        {
            edges.Add(($"binary{binary.Id}", $"binary{l.Id}"));
        }
        else if (!string.IsNullOrWhiteSpace(right) && right != "group")
        {
            edges.Add(($"binary{binary.Id}", right));
            //sb.AppendLine($"binary{binary.Id} -- {right}");
        }


        return "";
    }

    public string VisitGrouping(GroupingExpr grouping)
    {
        grouping.Expression.Accept(this);
        return "group";
    }

    public string VisitVariable(VariableExpr variable)
    {
        return $"\"{variable.Name.Lexeme}\"";
    }
}