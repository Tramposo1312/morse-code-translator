using System.ComponentModel.Composition;
using System.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace PawnExtension
{
    internal static class FileAndContentTypeDefinitions
    {
        [Export]
        [Name("pawn")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition PawnContentTypeDefinition = null;

        [Export]
        [FileExtension(".pwn")]
        [ContentType("pawn")]
        internal static FileExtensionToContentTypeDefinition PwnFileExtensionDefinition = null;

        [Export]
        [FileExtension(".inc")]
        [ContentType("pawn")]
        internal static FileExtensionToContentTypeDefinition IncFileExtensionDefinition = null;
    }

    internal static class PawnClassificationDefinitions
    {
        [Export]
        [Name("pawn.keyword")]
        internal static ClassificationTypeDefinition KeywordDefinition = null;

        [Export]
        [Name("pawn.comment")]
        internal static ClassificationTypeDefinition CommentDefinition = null;

        [Export]
        [Name("pawn.string")]
        internal static ClassificationTypeDefinition StringDefinition = null;

        [Export]
        [Name("pawn.preprocessor")]
        internal static ClassificationTypeDefinition PreprocessorDefinition = null;

        [Export]
        [Name("pawn.number")]
        internal static ClassificationTypeDefinition NumberDefinition = null;

        [Export]
        [Name("pawn.operator")]
        internal static ClassificationTypeDefinition OperatorDefinition = null;
    }
}