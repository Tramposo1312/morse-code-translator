using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnoTools.src
{
    public static class PawnContentTypeDefinition
    {
        [Export(typeof(ContentTypeDefinition))]
        [Name("pawn")]
        [BaseDefinition("code")]
        public static ContentTypeDefinition PawnContentType = null;

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [FileExtension(".pwn")]
        [ContentType("pawn")]
        public static FileExtensionToContentTypeDefinition PawnFileExtension = null;

        [Export(typeof(FileExtensionToContentTypeDefinition))]
        [FileExtension(".inc")]
        [ContentType("pawn")]
        public static FileExtensionToContentTypeDefinition PawnIncludeFileExtension = null;
    }
}
