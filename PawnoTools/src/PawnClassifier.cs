using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PawnoTools.Classification
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("pawn")]
    internal class PawnClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty(() => new PawnClassifier(ClassificationRegistry));
        }
    }

    internal class PawnClassifier : IClassifier
    {
        private readonly IClassificationTypeRegistryService _registry;

        private static readonly string[] Keywords = { "if", "else", "for", "while", "do", "switch", "case", "default", "break", "continue", "return", "new", "const", "static", "public", "stock", "forward", "native" };
        private static readonly string[] Types = { "int", "float", "bool", "char", "void", "string" };
        private static readonly string[] Preprocessors = { "#include", "#define", "#undef", "#if", "#else", "#elif", "#endif", "#pragma" };

        internal PawnClassifier(IClassificationTypeRegistryService registry)
        {
            _registry = registry;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var result = new List<ClassificationSpan>();

            string text = span.GetText();

            // Keywords
            foreach (var keyword in Keywords)
            {
                foreach (var match in Regex.Matches(text, $@"\b{keyword}\b"))
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, span.Start + ((Match)match).Index, ((Match)match).Length), _registry.GetClassificationType("PawnKeyword")));
                }
            }

            // Types
            foreach (var type in Types)
            {
                foreach (var match in Regex.Matches(text, $@"\b{type}\b"))
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, span.Start + ((Match)match).Index, ((Match)match).Length), _registry.GetClassificationType("PawnType")));
                }
            }

            // Preprocessors
            foreach (var preprocessor in Preprocessors)
            {
                foreach (var match in Regex.Matches(text, $@"\b{preprocessor}\b"))
                {
                    result.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, span.Start + ((Match)match).Index, ((Match)match).Length), _registry.GetClassificationType("PawnPreprocessor")));
                }
            }

            // Strings
            foreach (Match match in Regex.Matches(text, @"""(?:\\.|[^""\\])*"""))
            {
                result.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("PawnString")));
            }

            // Comments
            foreach (Match match in Regex.Matches(text, @"//.*$", RegexOptions.Multiline))
            {
                result.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("PawnComment")));
            }

            foreach (Match match in Regex.Matches(text, @"/\*.*?\*/", RegexOptions.Singleline))
            {
                result.Add(new ClassificationSpan(new SnapshotSpan(span.Snapshot, span.Start + match.Index, match.Length), _registry.GetClassificationType("PawnComment")));
            }

            return result;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged { add { } remove { } }
    }

}
