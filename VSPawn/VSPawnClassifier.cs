using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace VSPawn
{
    internal class VSPawnClassifier : IClassifier
    {
        private readonly IClassificationTypeRegistryService _classificationTypeRegistry;

        private static readonly Dictionary<string, string> s_classificationMap = new Dictionary<string, string>
        {
            [@"^\s*#\s*\w+"] = "preprocessor",
            [@"<[^>]+>|""[^""]+"""] = "preprocessor-include",
            [@"\b(if|else|while|for|function|return)\b"] = "keyword",
            [@"\b(int|float|string|bool)\b"] = "type",
            [@"[+\-*/=<>!]=?"] = "operator",
            [@"//.*$"] = "comment",
            [@"/\*[\s\S]*?\*/"] = "comment",
            [@"""[^""\\]*(?:\\.[^""\\]*)*"""] = "string",
            [@"\b\d+(\.\d+)?\b"] = "number",
            [@"\b[a-zA-Z_][a-zA-Z0-9_]*\b"] = "identifier"
        };

        private readonly Dictionary<string, IClassificationType> _classificationTypes;

        public VSPawnClassifier(IClassificationTypeRegistryService registry)
        {
            _classificationTypeRegistry = registry;
            _classificationTypes = new Dictionary<string, IClassificationType>();

            foreach (var type in s_classificationMap.Values)
            {
                _classificationTypes[type] = _classificationTypeRegistry.GetClassificationType(type);
            }
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            try
            {
                var result = new List<ClassificationSpan>();
                string text = span.GetText();

                foreach (var entry in s_classificationMap)
                {
                    foreach (Match match in Regex.Matches(text, entry.Key))
                    {
                        result.Add(new ClassificationSpan(
                            new SnapshotSpan(span.Snapshot, span.Start + match.Index, match.Length),
                            _classificationTypes[entry.Value]));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetClassificationSpans: {ex}");
                return new List<ClassificationSpan>();
            }
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
    }
}