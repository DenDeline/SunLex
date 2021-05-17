using System;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using System.Collections.Generic;

namespace SunLex.WebAPI.Endpoints.Dictionary
{
    public class GetDictionaryByIdResponse
    {
        public IEnumerable<WordTranslationDto> WordsTranslations { get; set; }
        public string Name { get; internal set; }
        public Guid Id { get; internal set; }
    }
}
