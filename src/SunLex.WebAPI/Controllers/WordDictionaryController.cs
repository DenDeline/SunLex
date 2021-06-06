using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.ApplicationCore.WordDictionaryAggregate.Specifications;
using SunLex.SharedKernel.Dtos.WordDictionary;
using SunLex.SharedKernel.Interfaces;

namespace SunLex.WebAPI.Controllers
{
    [ApiController]
    public class WordDictionaryController: ControllerBase
    {
        private readonly IRepository<WordDictionary> _repository;
        private readonly ILogger<WordDictionaryController> _logger;
        private readonly IMapper _mapper;

        public WordDictionaryController(
            IRepository<WordDictionary> repository,
            ILogger<WordDictionaryController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        
        
        [HttpGet("/dict/{dictionaryName}", Name = nameof(GetWordDictionaryByName))]
        public async Task<ActionResult<ReadWordDictionaryDto>> GetWordDictionaryByName(
                [FromRoute] string dictionaryName,
                CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameSpec(dictionaryName);
            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (entity is null) return NotFound();
            
            var response = _mapper.Map<ReadWordDictionaryDto>(entity);
            
            return Ok(response);
        }

        [HttpGet("/dict")]
        public async Task<ActionResult<IEnumerable<ReadWordDictionaryDto>>> GetAllDictionaries(
            CancellationToken cancellationToken = new())
        {
            
            var entities = await _repository.ListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ReadWordDictionaryDto>>(entities);

            return Ok(result);
        }

        [HttpPost("/dict")]
        [ProducesResponseType(typeof(ReadWordDictionaryDto), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateWordDictionary(
            [FromBody] CreateWordDictionaryDto request, 
            CancellationToken cancellationToken = new())
        {
            var newWordDictionary = new WordDictionary(request.Name);
            var createdItem = await _repository.AddAsync(newWordDictionary, cancellationToken);

            var response = _mapper.Map<ReadWordDictionaryDto>(createdItem);

            return CreatedAtRoute(
                nameof(GetWordDictionaryByName), 
                new { dictionaryName = response.Name }, 
                response
            );
        }

        [HttpPut("/dict/{dictionaryId:guid}")]
        public async Task<ActionResult> UpdateWordDictionaryById(
            [FromRoute] Guid dictionaryId,
            [FromBody] UpdateWordDictionaryDto dto,
            CancellationToken cancellationToken = new())
        {
            var entity = await _repository.GetByIdAsync(dictionaryId, cancellationToken);

            if (entity is null) return NotFound();

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