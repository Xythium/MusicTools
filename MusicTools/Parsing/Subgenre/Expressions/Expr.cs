﻿namespace MusicTools.Parsing.Subgenre.Expressions;

public abstract class Expr
{
    public abstract string Accept(Visitor visitor);
}