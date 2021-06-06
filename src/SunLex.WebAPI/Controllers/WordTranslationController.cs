using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.ApplicationCore.WordDictionaryAggregate.Specifications;
using SunLex.SharedKernel.Dtos.WordDictionary;
using SunLex.SharedKernel.Dtos.WordTranslation;
using SunLex.SharedKernel.Interfaces;

namespace SunLex.WebAPI.Controllers
{
    [ApiController]
    public class WordTranslationController: ControllerBase
    {
        private readonly IRepository<WordDictionary> _repository;
        private readonly IMapper _mapper;

        public WordTranslationController(
            IRepository<WordDictionary> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/dict/{dictionaryName}/translations/", Name = nameof(GetAllWordsTranslations))]
        public async Task<ActionResult<IEnumerable<ReadWordTranslationDto>>> GetAllWordsTranslations(
            [FromRoute] string dictionaryName,
            CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameWithWordsSpec(dictionaryName);
            var dictionary = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (dictionary is null) return NotFound();
            
            var response = _mapper.Map<IEnumerable<ReadWordTranslationDto>>(dictionary.WordsTranslations);
            
            return Ok(response);
        }
        
        
        [HttpGet("/dict/{dictionaryName}/translations/{translationId:int}", Name = nameof(GetWordTranslationById))]
        public async Task<ActionResult<ReadWordTranslationDto>> GetWordTranslationById(
            [FromRoute] string dictionaryName,
            [FromRoute] int translationId,
            CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameWithWordsSpec(dictionaryName);
            var dictionary = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (dictionary is null) return NotFound();
            
            var wordTranslation = dictionary.GetWordTranslationById(translationId);

            if (wordTranslation is null) return NotFound();

            var response = _mapper.Map<ReadWordDictionaryDto>(wordTranslation);
            
            return Ok(response);
        }
        
        [HttpPost("/dict/{dictionaryName}/translations/")]
        public async Task<ActionResult> CreateWordTranslation(
            [FromRoute] string dictionaryName,
            [FromBody] CreateWordTranslationDto dto,
            CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameWithWordsSpec(dictionaryName);
            var dictionary = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (dictionary is null) return NotFound();

            var newWordTranslation = new WordTranslation(dto.FromWord, dto.ToWord);
            
            dictionary.AddWordTranslation(newWordTranslation);

            await _repository.UpdateAsync(dictionary, cancellationToken);

            var response = _mapper.Map<ReadWordTranslationDto>(newWordTranslation);

            return CreatedAtRoute(
                nameof(GetWordTranslationById), 
                new {dictionaryName = dictionaryName, translationId = response.Id},
                response);
        }
    }
}