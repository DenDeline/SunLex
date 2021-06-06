using System.ComponentModel.DataAnnotations;

namespace SunLex.SharedKernel.Dtos.WordDictionary
{
    public record UpdateWordDictionaryDto
    {
        [Required]
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public string? ThumbnailImageUrl { get; init; }
    }
}