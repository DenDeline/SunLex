using Ardalis.GuardClauses;
using SunLex.SharedKernel;
using SunLex.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class WordDictionary: BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        private readonly List<WordTranslation> _wordsTranslations = new();
        public IReadOnlyCollection<WordTranslation> WordsTranslations => _wordsTranslations.AsReadOnly();

        public WordDictionary(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }

        public void AddWordTranslation(WordTranslation newWordTranslation)
        {
            Guard.Against.Null(newWordTranslation, nameof(newWordTranslation));
            _wordsTranslations.Add(newWordTranslation);
        }
            
        public void UpdateName(string newName)
        {
            Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
        }
    }
}
