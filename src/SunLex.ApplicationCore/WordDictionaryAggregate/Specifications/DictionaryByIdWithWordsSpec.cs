using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunLex.ApplicationCore.WordDictionaryAggregate.Specifications
{
    public class DictionaryByIdWithWordsSpec: Specification<WordDictionary>, ISingleResultSpecification
    {
        public DictionaryByIdWithWordsSpec(int dictionaryId)
        {
            Query
                .Where(dict => dict.Id == dictionaryId)
                .Include(dict => dict.WordsTranslations);
        }
    }
}
