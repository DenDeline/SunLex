using Ardalis.GuardClauses;
using SunLex.SharedKernel;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class WordTranslation: BaseEntity
    {
        private WordTranslation() { }
        
        public WordTranslation(Word fromWord, Word toWord)
        {
            FromWord = Guard.Against.Null(fromWord, nameof(fromWord));
            ToWord = Guard.Against.Null(toWord, nameof(toWord));
        }

        public Word FromWord { get; private set; }
        public Word ToWord { get; private set; }
        
        public string? Description { get; private set; }

        public void UpdateDescription(string description)
        {
            Description = Guard.Against.Default(description, nameof(description));
        }
    }
}
