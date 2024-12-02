using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicTools.Parsing.Track.Expressions;
using MusicTools.Parsing.Track.Statements;
using Newtonsoft.Json;

namespace MusicTools.Parsing.Track;

public class Reconstructor : IExprVisitor, IStmtVisitor
{
    public readonly TrackInfoNew TrackInfo = new()
    {
        Artists = new List<ArtistInfo>()
    };

    private bool isGroup = false;
    private bool isFeat = false;

    public string FromString(string str)
    {
        var scanner = new Scanner(str);
        return FromScanner(scanner);
    }

    public string FromScanner(Scanner scanner)
    {
        var tokens = scanner.ScanTokens().ToArray();
#if NET5_0_OR_GREATER
        var parser = new Parser();
        return FromParser(parser, tokens);
#else
        var parser = new Parser(tokens);
        return FromParser(parser, tokens);
#endif
    }

#if NET5_0_OR_GREATER
    public string FromParser(Parser parser, ReadOnlySpan<Token> tokens)
#else
    public string FromParser(Parser parser, Token[] tokens)
#endif
    {
        var stmts = parser.Parse(tokens);
        var processor = new Processor(stmts);
        return FromProcessor(processor);
    }

    public string FromProcessor(Processor processor)
    {
        var stmts = processor.Rearrange();
        var reconstructed = Reconstruct(stmts);
        return reconstructed;
    }

    public string Reconstruct(List<Stmt> statements)
    {
        var sb = new StringBuilder();
        foreach (var stmt in statements)
        {
            var text = stmt.Accept(this);
            if (!text.Contains("feat."))
            {
                sb.Append(text);
                sb.Append(' ');
            }
        }

        TrackInfo.Title = sb.ToString().Trim();

        return TrackInfo.Title;
    }

    public string VisitExpr(ExprStmt expr)
    {
        return expr.Expression.Accept(this);
    }

    public string VisitFeat(FeatStmt feat)
    {
        return "featstmt";
    }

    public string VisitFeat(FeatExpr feat)
    {
        var name = feat.Artist.Accept(this);
        TrackInfo.Artists.Add(new ArtistInfo
        {
            Name = name,
            Join = "feat."
        });
        return $"feat. {name}";
    }

    public string VisitGrouping(GroupingExpr grouping)
    {
        var group = grouping.Group.Accept(this);
        return $"({group})";
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

public class TrackInfoNew : IEquatable<TrackInfoNew>
{
    public required IList<ArtistInfo> Artists { get; init; }

    public string Title { get; set; }


    public bool Equals(TrackInfoNew other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Artists.Equals(other.Artists);
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != typeof(TrackInfoNew))
            return false;
        return Equals((TrackInfoNew)obj);
    }

    public override int GetHashCode()
    {
        return Artists.GetHashCode();
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class ArtistInfo : IEquatable<ArtistInfo>
{
    public required string Name { get; init; }

    public string Join { get; init; }


    public bool Equals(ArtistInfo other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Name == other.Name && Join == other.Join;
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != typeof(ArtistInfo))
            return false;
        return Equals((ArtistInfo)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Name.GetHashCode() * 397) ^ Join.GetHashCode();
        }
    }
}