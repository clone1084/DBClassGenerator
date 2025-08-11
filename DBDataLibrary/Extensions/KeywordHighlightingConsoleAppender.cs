using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using System.Text.RegularExpressions;

namespace DBDataLibrary.Extensions
{
    public class KeywordHighlightingConsoleAppender : AppenderSkeleton
    {
        public PatternLayout LayoutPattern { get; set; }

        private readonly Dictionary<Level, (ConsoleColor Fore, ConsoleColor Back)> _levelColors = new();
        private readonly List<(Regex Pattern, ConsoleColor Fore, ConsoleColor Back)> _highlightKeywords = new();

        // ====== CLASSI PER MAPPING CONFIGURABILE ======
        public class LevelColorMapping
        {
            public Level Level { get; set; }
            public string ForeColor { get; set; }
            public string BackColor { get; set; }
        }

        public class KeywordColorMapping
        {
            public string StringToMatch { get; set; }
            public string ForeColor { get; set; }
            public string BackColor { get; set; }
        }

        // ====== CHIAMATI DA log4net DURANTE CONFIGURAZIONE XML ======
        public void AddMapping(LevelColorMapping mapping)
        {
            if (mapping?.Level != null)
            {
                _levelColors[mapping.Level] = (
                    ParseConsoleColor(mapping.ForeColor),
                    ParseConsoleColor(mapping.BackColor)
                );
            }
        }

        public void AddKeywordMapping(KeywordColorMapping mapping)
        {
            if (!string.IsNullOrWhiteSpace(mapping?.StringToMatch))
            {
                // Match della keyword ovunque nella parola, ma evidenzia l'intera parola
                var regex = new Regex(@"\b\w*" + Regex.Escape(mapping.StringToMatch) + @"\w*\b", RegexOptions.IgnoreCase);
                _highlightKeywords.Add((
                    regex,
                    ParseConsoleColor(mapping.ForeColor),
                    ParseConsoleColor(mapping.BackColor)
                ));
            }
        }


        protected override void Append(LoggingEvent loggingEvent)
        {
            var writer = Console.Out;

            var originalFore = Console.ForegroundColor;
            var originalBack = Console.BackgroundColor;

            // Colore di default per il livello
            if (_levelColors.TryGetValue(loggingEvent.Level, out var colors))
            {
                Console.ForegroundColor = colors.Fore;
                if (colors.Back != default)
                    Console.BackgroundColor = colors.Back;
            }

            var message = RenderLoggingEvent(loggingEvent);
            WriteWithHighlights(message, writer, originalFore, originalBack);

            Console.ForegroundColor = originalFore;
            Console.BackgroundColor = originalBack;
        }

        private void WriteWithHighlights(string message, TextWriter writer, ConsoleColor defaultFore, ConsoleColor defaultBack)
        {
            int currentIndex = 0;

            // Trova tutti i match delle parole chiave
            var matches = _highlightKeywords
                .SelectMany(kvp => kvp.Pattern.Matches(message)
                    .Cast<Match>()
                    .Select(m => (Index: m.Index, Length: m.Length, Fore: kvp.Fore, Back: kvp.Back)))
                .OrderBy(m => m.Index)
                .ToList();

            foreach (var match in matches)
            {
                if (match.Index > currentIndex)
                {
                    writer.Write(message.Substring(currentIndex, match.Index - currentIndex));
                }

                var prevFore = Console.ForegroundColor;
                var prevBack = Console.BackgroundColor;

                Console.ForegroundColor = match.Fore != default ? match.Fore : defaultFore;
                if (match.Back != default)
                    Console.BackgroundColor = match.Back;

                writer.Write(message.Substring(match.Index, match.Length));

                Console.ForegroundColor = prevFore;
                Console.BackgroundColor = prevBack;

                currentIndex = match.Index + match.Length;
            }

            if (currentIndex < message.Length)
            {
                writer.Write(message.Substring(currentIndex));
            }
        }

        private ConsoleColor ParseConsoleColor(string colorName)
        {
            if (string.IsNullOrWhiteSpace(colorName))
                return default;

            return Enum.TryParse(colorName, true, out ConsoleColor color) ? color : default;
        }
    }
}
