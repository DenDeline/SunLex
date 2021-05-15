using Ardalis.GuardClauses;
using SunLex.SharedKernel;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class Word: BaseEntity
    {
        private Word()
        {

        }

        public Word(string spelling)
        {
            Spelling = Guard.Against.NullOrEmpty(spelling, nameof(spelling));
        }

        public string Spelling { get; private set; }
    }
}
