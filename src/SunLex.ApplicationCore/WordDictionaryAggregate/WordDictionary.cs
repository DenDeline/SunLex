using System;
using Ardalis.GuardClauses;
using SunLex.SharedKernel;
using SunLex.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class WordDictionary: BaseEntity<int>, IAggregateRoot
    {
        public string Name { get; private set; }
        public string? Description { get; private set; } 
        public string? ThumbnailImageUrl { get; private set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; }
        
        private readonly List<WordTranslation> _wordsTranslations = new();
        public IReadOnlyCollection<WordTranslation> WordsTranslations => _wordsTranslations.AsReadOnly();
        
        public WordDictionary(string name)
        {
            Name = Guard.Against.NullOrEmpty(name.Trim(), nameof(name));
        }

        public void AddWordTranslation(WordTranslation newWordTranslation)
        {
            Guard.Against.Null(newWordTranslation, nameof(newWordTranslation));
            _wordsTranslations.Add(newWordTranslation);
        }

        public bool RemoveWordTranslation(WordTranslation deletingWordTranslation)
        {
            Guard.Against.Null(deletingWordTranslation, nameof(deletingWordTranslation));
            return _wordsTranslations.Remove(deletingWordTranslation);
        }

        public void UpdateName(string newName)
        {
            Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
        }

        public void UpdateThumbnailImageUrl(string? newThumbnailImageUrl)
        {
            ThumbnailImageUrl = string.IsNullOrEmpty(newThumbnailImageUrl) ? null : newThumbnailImageUrl;
        }
        
        public void UpdateDescription(string? newDescription)
        {
            Description = string.IsNullOrEmpty(newDescription) ? null : newDescription;
        }
    }
}
