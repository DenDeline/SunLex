using System;

namespace SunLex.SharedKernel.Dtos.WordTranslation
{
    public record ReadWordTranslationDto
    {
        public Guid Id { get; init; }
        public string FromWord { get; init; }
        public string ToWord { get; init; }
    }
}