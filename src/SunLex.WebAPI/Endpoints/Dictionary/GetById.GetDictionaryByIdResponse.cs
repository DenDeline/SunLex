using SunLex.ApplicationCore.WordDictionaryAggregate;
using System.Collections.Generic;

namespace SunLex.WebAPI.Endpoints.Dictionary
{
    public class GetDictionaryByIdResponse
    {
        public IReadOnlyCollection<WordTranslation> WordsTranslations { get; set; }
        public string Name { get; internal set; }
        public int Id { get; internal set; }
    }
}
