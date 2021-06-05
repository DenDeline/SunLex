using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.ApplicationCore.WordDictionaryAggregate.Specifications;
using SunLex.SharedKernel.Dtos.WordDictionary;
using SunLex.SharedKernel.Dtos.WordTranslation;
using SunLex.SharedKernel.Interfaces;

namespace SunLex.WebAPI.Controllers
{
    [ApiController]
    public class WordDictionaryController: ControllerBase
    {
        private readonly IRepository<WordDictionary> _repository;
        private readonly ILogger<WordDictionaryController> _logger;

        public WordDictionaryController(
            IRepository<WordDictionary> repository,
            ILogger<WordDictionaryController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        
        [HttpGet("/dictionaries/{dictionaryId:guid}")]
        public async Task<ActionResult<ReadWordDictionaryDto>> GetWordDictionaryById(
            [FromRoute] Guid dictionaryId, 
            CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByIdWithWordsSpec(dictionaryId);
            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (entity == null) return NotFound();

            var response = new ReadWordDictionaryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                WordsTranslations = entity.WordsTranslations.Select(t => new ReadWordTranslationDto
                {
                    Id = t.Id,
                    FromWord = t.FromWord.ToString(),
                    ToWord = t.ToWord.ToString(),
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet("/dictionaries/{dictionaryName}", Name = nameof(GetWordDictionaryByName))]
        public async Task<ActionResult<ReadWordDictionaryDto>> GetWordDictionaryByName(
                [FromRoute] string dictionaryName,
                CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameWithWordsSpec(dictionaryName);
            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (entity is null)
            {
                return NotFound();
            }   
            
            var response = new ReadWordDictionaryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                WordsTranslations = entity.WordsTranslations.Select(t => new ReadWordTranslationDto
                {
                    Id = t.Id,
                    FromWord = t.FromWord.ToString(),
                    ToWord = t.ToWord.ToString(),
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost("/dictionaries")]
        [ProducesResponseType(typeof(ReadWordDictionaryDto), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateWordDictionary(
            [FromBody] CreateWordDictionaryDto request, 
            CancellationToken cancellationToken = new())
        {
            var newWordDictionary = new WordDictionary(request.Name);
            var createdItem = await _repository.AddAsync(newWordDictionary, cancellationToken);

            var response = new ReadWordDictionaryDto
            {
                Id = createdItem.Id,
                Name = createdItem.Name
            };

            return CreatedAtRoute(
                "GetWordDictionaryByName", 
                new { dictionaryName = response.Name }, 
                response
            );
        }

        [HttpPut("/dictionaries/{dictionaryId:guid}")]
        public async Task<ActionResult> UpdateWordDictionaryById(
            [FromRoute] Guid dictionaryId,
            [FromBody] UpdateWordDictionaryDto dto,
            CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByIdWithWordsSpec(dictionaryId);

            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (entity is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                entity.UpdateName(dto.Name);
            }
            
            entity.UpdateDescription(dto.Description);
            entity.UpdateThumbnailImageUrl(dto.ThumbnailImageUrl);


            await _repository.UpdateAsync(entity, cancellationToken);

            return Ok();
        }
    }
}