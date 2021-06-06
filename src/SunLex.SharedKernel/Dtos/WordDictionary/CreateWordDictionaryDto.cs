using System.ComponentModel.DataAnnotations;

namespace SunLex.SharedKernel.Dtos.WordDictionary
{
    public record CreateWordDictionaryDto
    {
        [Required]
        public string Name { get; init; } = null!;
    }
}