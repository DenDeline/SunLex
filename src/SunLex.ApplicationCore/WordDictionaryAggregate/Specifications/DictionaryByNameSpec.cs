using Ardalis.Specification;

namespace SunLex.ApplicationCore.WordDictionaryAggregate.Specifications
{
    public class DictionaryByNameSpec: Specification<WordDictionary>, ISingleResultSpecification
    {
        public DictionaryByNameSpec(string dictionaryName)
        {
            Query
                .Where(dict => dict.Name == dictionaryName);
        }
    }
}