using System;

namespace MusicTools.Parsing.Track;

public class ParseError(string message) : Exception(message);