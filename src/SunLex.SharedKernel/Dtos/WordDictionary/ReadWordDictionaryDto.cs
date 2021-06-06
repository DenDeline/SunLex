namespace SunLex.SharedKernel.Dtos.WordDictionary
{
    public record ReadWordDictionaryDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }
        public string? ThumbnailImageUrl { get; init; }
        public string WordsTranslationsUrl { get; init; } = null!;
    }
}