using Ardalis.Specification;

namespace SunLex.ApplicationCore.WordDictionaryAggregate.Specifications
{
    public class DictionaryByNameWithWordsSpec: Specification<WordDictionary>, ISingleResultSpecification
    {
        public DictionaryByNameWithWordsSpec(string dictionaryName)
        {
            Query
                .Where(dict => dict.Name == dictionaryName)
                .Include(dict => dict.WordsTranslations);
        }
    }
}