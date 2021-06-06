using Ardalis.GuardClauses;
using SunLex.SharedKernel;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class Word: BaseEntity<int>
    {
        public string Spelling { get; private set; }
        private Word () {  }
        public Word(string spelling)
        {
            Spelling = Guard.Against.NullOrEmpty(spelling, nameof(spelling));
        }
        
        public override string ToString()
            => Spelling;

        public static implicit operator Word(string wordToConvert)
            => new(wordToConvert);
    }
}
