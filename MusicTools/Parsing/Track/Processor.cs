using System;
using System.Collections.Generic;
using System.Linq;
using MusicTools.Parsing.Track.Expressions;
using MusicTools.Parsing.Track.Statements;

namespace MusicTools.Parsing.Track;

public class Processor(List<Stmt> stmts) : IStmtVisitor
{
    public List<Stmt> Rearrange()
    {
        var rearranged = new List<Stmt>();
        
       

        return stmts;
    }

    public string VisitExpr(ExprStmt expr)
    {
        throw new System.NotImplementedException();
    }

    public string VisitFeat(FeatStmt feat)
    {
        throw new System.NotImplementedException();
    }
}