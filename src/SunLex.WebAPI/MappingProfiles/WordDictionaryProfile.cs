using AutoMapper;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.SharedKernel.Dtos.WordDictionary;
using SunLex.WebAPI.Helpers;

namespace SunLex.WebAPI.MappingProfiles
{
    public class WordDictionaryProfile: Profile
    {
        public WordDictionaryProfile()
        {
            CreateMap<WordDictionary, ReadWordDictionaryDto>()
                .ForMember(
                    dest => dest.WordsTranslationsUrl, 
                    e => e.MapFrom(source => WebApiUrlHelper.GetWordsTranslationsUrl(source.Name)));
            
        }
    }
}