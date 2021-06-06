using Ardalis.Specification;

namespace SunLex.ApplicationCore.WordDictionaryAggregate.Specifications
{
    public class DictionaryByNameWithWordsSpec: Specification<WordDictionary>, ISingleResultSpecification
    {
        public DictionaryByNameWithWordsSpec(string dictionaryName)
        {
            var query = Query
                .Where(dict => dict.NormalizedName == dictionaryName.Trim().ToUpper().Normalize());

            query.Include(dict => dict.WordsTranslations).ThenInclude(tr => tr.FromWord);
            query.Include(dict => dict.WordsTranslations).ThenInclude(tr => tr.ToWord);
        }
    }
}