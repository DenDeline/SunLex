using System.ComponentModel.DataAnnotations;

namespace SunLex.SharedKernel.Dtos.WordTranslation
{
    public record CreateWordTranslationDto
    {
        [Required] public string FromWord { get; init; } = null!;

        [Required] public string ToWord { get; init; } = null!;
    }
}