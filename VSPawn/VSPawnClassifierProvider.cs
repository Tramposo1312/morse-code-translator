using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace VSPawn
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("pawn")]
    internal class VSPawnClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry { get; set; }

        [Export]
        [Name("pawn")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition PawnContentType = null;

        [Export]
        [FileExtension(".pwn")]
        [ContentType("pawn")]
        internal static FileExtensionToContentTypeDefinition PawnSourceFileExtension = null;

        [Export]
        [FileExtension(".inc")]
        [ContentType("pawn")]
        internal static FileExtensionToContentTypeDefinition PawnIncludeFileExtension = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("preprocessor")]
        internal static ClassificationTypeDefinition PreprocessorClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("preprocessor-include")]
        internal static ClassificationTypeDefinition PreprocessorIncludeClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("keyword")]
        internal static ClassificationTypeDefinition KeywordClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("type")]
        internal static ClassificationTypeDefinition TypeClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("operator")]
        internal static ClassificationTypeDefinition OperatorClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("comment")]
        internal static ClassificationTypeDefinition CommentClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("string")]
        internal static ClassificationTypeDefinition StringClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("number")]
        internal static ClassificationTypeDefinition NumberClassificationType = null;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("identifier")]
        internal static ClassificationTypeDefinition IdentifierClassificationType = null;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            var classifier = buffer.Properties.GetOrCreateSingletonProperty<VSPawnClassifier>(
                creator: () => {
                    System.Diagnostics.Debug.WriteLine("Creating VSPawnClassifier");
                    return new VSPawnClassifier(ClassificationRegistry);
                });
            System.Diagnostics.Debug.WriteLine("Returning VSPawnClassifier");
            return classifier;
        }
    }


}