using System;
using Ardalis.Specification;

namespace SunLex.ApplicationCore.WordDictionaryAggregate.Specifications
{
    public class DictionaryByIdWithWordsSpec: Specification<WordDictionary>, ISingleResultSpecification
    {
        public DictionaryByIdWithWordsSpec(Guid dictionaryId)
        {
            Query
                .Where(dict => dict.Id == dictionaryId)
                .Include(dict => dict.WordsTranslations);
        }
    }
}
