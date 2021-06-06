using System;

namespace SunLex.SharedKernel.Dtos.WordDictionary
{
    public record ReadWordDictionaryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string ThumbnailImageUrl { get; init; }
        public string WordsTranslationsUrl { get; init; }
    }
}