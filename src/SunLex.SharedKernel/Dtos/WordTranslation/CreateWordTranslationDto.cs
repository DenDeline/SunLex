namespace SunLex.SharedKernel.Dtos.WordTranslation
{
    public record CreateWordTranslationDto
    {
        public string FromWord { get; init; }
        public string ToWord { get; init; }
    }
}