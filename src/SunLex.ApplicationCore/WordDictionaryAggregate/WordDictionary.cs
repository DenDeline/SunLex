using Ardalis.GuardClauses;
using SunLex.SharedKernel;
using SunLex.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SunLex.ApplicationCore.WordDictionaryAggregate
{
    public class WordDictionary: BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }
        public string? Description { get; private set; } 
        public string? ThumbnailImageUrl { get; private set; }

        private readonly List<WordTranslation> _wordsTranslations = new();
        public IReadOnlyCollection<WordTranslation> WordsTranslations => _wordsTranslations.AsReadOnly();
        
        public WordDictionary(string name)
        {
            Name = Guard.Against.NullOrEmpty(name.Trim(), nameof(name));
            NormalizedName = name.Trim().ToUpper().Normalize();
        }

        public WordTranslation? GetWordTranslationById(int wordTranslationId)
            => WordsTranslations.FirstOrDefault(translation => translation.Id == wordTranslationId);

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

        public void UpdateThumbnailImageUrl(string newImageUrl)
        {
            var trimNewImageUrl = newImageUrl.Trim();
            ThumbnailImageUrl = string.IsNullOrEmpty(trimNewImageUrl) ? null : trimNewImageUrl;
        }
        
        public void UpdateDescription(string newDescription)
        {
            var trimNewDescription = newDescription.Trim();
            Description = string.IsNullOrEmpty(trimNewDescription) ? null : trimNewDescription;
        }
    }
}
