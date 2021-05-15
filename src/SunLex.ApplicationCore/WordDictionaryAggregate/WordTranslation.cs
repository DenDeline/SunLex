using Ardalis.GuardClauses;
using SunLex.SharedKernel;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class WordTranslation: BaseEntity
    {
        private WordTranslation()
        {

        }

        public WordTranslation(Word fromWord, Word toWord)
        {
            FromWord = Guard.Against.Null(fromWord, nameof(fromWord));
            ToWord = Guard.Against.Null(toWord, nameof(toWord));
        }

        public Word FromWord { get; private set; }
        public Word ToWord { get; private set; }
    }
}
