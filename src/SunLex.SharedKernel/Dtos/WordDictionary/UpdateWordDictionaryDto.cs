namespace SunLex.SharedKernel.Dtos.WordDictionary
{
    public record UpdateWordDictionaryDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string ThumbnailImageUrl { get; init; }
    }
}