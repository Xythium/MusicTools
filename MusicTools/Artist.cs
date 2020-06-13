using System;

namespace MusicTools
{
    public class Artist
    {
        private const StringComparison COMPARISON_TYPE = StringComparison.OrdinalIgnoreCase;
        
        public static string ForceCasing(string s)
        {
            return
                s.Replace("Mr. Bill", "Mr. Bill")
                    .Replace("KUURO", "KUURO")
                    .Replace("Mad Zach", "Mad Zach")
                    .Replace("EDDIE", "EDDIE")
                    .Replace("Vorso", "Vorso")
                    .Replace("yunis", "yunis")
                    .Replace("CHEE", "CHEE")
                    .Replace("REZZ", "REZZ")
                    .Replace("i_o", "i_o")
                    .Replace("HEYZ", "HEYZ")
                    .Replace("PROKO", "PROKO")
                    .Replace("SKEW", "SKEW")
                    .Replace("ZEKE BEATS", "ZEKE BEATS")
                    .Replace("EPROM", "EPROM")
                    .Replace("G Jones", "G Jones")
                    .Replace("k?d", "k?d")
                    .Replace("MOGUAI", "MOGUAI")
                    .Replace("Monstergetdown", "Monstergetdown") // Monstergetdown?
                    .Replace("Rhett", "Rhett")
                    .Replace("Rinzen", "Rinzen")
                    .Replace("TESTPILOT", "TESTPILOT")
                    .Replace("deadmau5", "deadmau5");
        }
    }
}