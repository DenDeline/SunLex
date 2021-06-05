using System;
using System.Collections.Generic;
using SunLex.SharedKernel.Dtos.WordTranslation;

namespace SunLex.SharedKernel.Dtos.WordDictionary
{
    public record ReadWordDictionaryDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<ReadWordTranslationDto> WordsTranslations { get; init; }
    }
}