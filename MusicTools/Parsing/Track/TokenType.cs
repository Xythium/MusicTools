namespace MusicTools.Parsing.Track;

public enum TokenType
{
    LeftParen, // (
    RightParen, // )
    LeftBrace, // [
    RightBrace, // ]
    
    Featuring, // featuring
    FeatDot, // feat.
    Feat, // feat
    FtDot, // ft.
    Ft, // ft
    Ampersand, // &
    And, // and
    Versus, // vs.
    
    Remix, // Remix
    Vip, // VIP
    ChillMix, // Chill Mix
    
    Identifier,
    
    Eof
}

/*
title: name (feat. artist*)? (artist* remix)?
*/