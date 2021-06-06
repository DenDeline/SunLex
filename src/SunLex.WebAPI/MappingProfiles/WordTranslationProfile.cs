using AutoMapper;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.SharedKernel.Dtos.WordTranslation;

namespace SunLex.WebAPI.MappingProfiles
{
    public class WordTranslationProfile: Profile
    {
        public WordTranslationProfile()
        {
            CreateMap<WordTranslation, ReadWordTranslationDto>();
        }
    }
}