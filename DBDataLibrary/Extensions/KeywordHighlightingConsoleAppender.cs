using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DBDataLibrary.Extensions
{
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class KeywordHighlightingConsoleAppender : AppenderSkeleton
    {
        public PatternLayout LayoutPattern { get; set; }

        // Colori per livelli di log
        private static readonly Dictionary<Level, ConsoleColor> _levelColors = new()
        {
            [Level.Debug] = ConsoleColor.Gray,
            [Level.Info] = ConsoleColor.White,
            [Level.Warn] = ConsoleColor.Yellow,
            [Level.Error] = ConsoleColor.Red,
            [Level.Fatal] = ConsoleColor.Red
        };

        // Colori per parole chiave nel messaggio
        private static readonly List<(Regex Pattern, ConsoleColor Color)> _highlightKeywords = new()
        {
            (new Regex(@"\bOK\b", RegexOptions.IgnoreCase), ConsoleColor.Green),
            (new Regex(@"\bsuccess\b", RegexOptions.IgnoreCase), ConsoleColor.Green),

            (new Regex(@"\bKO\b", RegexOptions.IgnoreCase), ConsoleColor.Red),
            (new Regex(@"\bfail\b", RegexOptions.IgnoreCase), ConsoleColor.Red),
            (new Regex(@"\bfailed\b", RegexOptions.IgnoreCase), ConsoleColor.Red),
            (new Regex(@"\berror\b", RegexOptions.IgnoreCase), ConsoleColor.Red),
        };

        protected override void Append(LoggingEvent loggingEvent)
        {
            var writer = Console.Out;
            var originalColor = Console.ForegroundColor;

            // Colore per il livello di log
            if (_levelColors.TryGetValue(loggingEvent.Level, out var levelColor))
            {
                Console.ForegroundColor = levelColor;
            }

            var message = RenderLoggingEvent(loggingEvent);
            WriteWithHighlights(message, writer, originalColor);

            Console.ForegroundColor = originalColor;
        }

        private void WriteWithHighlights(string message, TextWriter writer, ConsoleColor defaultColor)
        {
            int currentIndex = 0;

            // Trova tutti i match delle parole chiave
            var matches = _highlightKeywords
                .SelectMany(kvp => kvp.Pattern.Matches(message)
                    .Cast<Match>()
                    .Select(m => (Index: m.Index, Length: m.Length, Color: kvp.Color)))
                .OrderBy(m => m.Index)
                .ToList();

            foreach (var match in matches)
            {
                if (match.Index > currentIndex)
                {
                    writer.Write(message.Substring(currentIndex, match.Index - currentIndex));
                }

                var prevColor = Console.ForegroundColor;
                Console.ForegroundColor = match.Color;
                writer.Write(message.Substring(match.Index, match.Length));
                Console.ForegroundColor = prevColor;

                currentIndex = match.Index + match.Length;
            }

            if (currentIndex < message.Length)
            {
                writer.Write(message.Substring(currentIndex));
            }

            //writer.WriteLine();
        }
    }


}
