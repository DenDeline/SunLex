using System;

namespace SunLex.WebAPI.Endpoints.Dictionary
{
    public class WordTranslationDto
    {
        public Guid Id { get; set; }
        public string FromWord { get; set; }
        public string ToWord { get; set; }
    }
}