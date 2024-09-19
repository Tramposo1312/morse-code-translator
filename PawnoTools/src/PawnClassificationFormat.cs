using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PawnoTools.src
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "PawnKeyword")]
    [Name("PawnKeyword")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class PawnKeywordFormat : ClassificationFormatDefinition
    {
        public PawnKeywordFormat()
        {
            DisplayName = "Pawn Keyword";
            ForegroundColor = Colors.Blue;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "PawnType")]
    [Name("PawnType")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class PawnTypeFormat : ClassificationFormatDefinition
    {
        public PawnTypeFormat()
        {
            DisplayName = "Pawn Type";
            ForegroundColor = Colors.Teal;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "PawnPreprocessor")]
    [Name("PawnPreprocessor")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class PawnPreprocessorFormat : ClassificationFormatDefinition
    {
        public PawnPreprocessorFormat()
        {
            DisplayName = "Pawn Preprocessor";
            ForegroundColor = Colors.DarkMagenta;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "PawnString")]
    [Name("PawnString")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class PawnStringFormat : ClassificationFormatDefinition
    {
        public PawnStringFormat()
        {
            DisplayName = "Pawn String";
            ForegroundColor = Colors.Brown;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "PawnComment")]
    [Name("PawnComment")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class PawnCommentFormat : ClassificationFormatDefinition
    {
        public PawnCommentFormat()
        {
            DisplayName = "Pawn Comment";
            ForegroundColor = Colors.Green;
        }
    }

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("PawnKeyword")]
    internal static class PawnKeywordClassificationDefinition { }

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("PawnType")]
    internal static class PawnTypeClassificationDefinition { }

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("PawnPreprocessor")]
    internal static class PawnPreprocessorClassificationDefinition { }

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("PawnString")]
    internal static class PawnStringClassificationDefinition { }

    [Export(typeof(ClassificationTypeDefinition))]
    [Name("PawnComment")]
    internal static class PawnCommentClassificationDefinition { }
}
