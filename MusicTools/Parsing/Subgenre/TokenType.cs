namespace MusicTools.Parsing.Subgenre;

public enum TokenType
{
    LeftParen, // (
    RightParen, // )

    Plus, // +
    Greater, // >
    Tilde, // ~
    
    PlusGreater, // +>
    GreaterPlus, // >+
    PlusTilde, // +~
    TildePlus, // ~+
    
    Identifier,
    
    Eof
}