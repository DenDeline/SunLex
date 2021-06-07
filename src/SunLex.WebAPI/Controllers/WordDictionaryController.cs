using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SunLex.ApplicationCore.Interfaces;
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
        private readonly ILearningResourcesService _learningResourcesService;
        private readonly IMapper _mapper;

        public WordDictionaryController(
            IRepository<WordDictionary> repository,
            ILearningResourcesService learningResourcesService,
            IMapper mapper)
        {
            _repository = repository;
            _learningResourcesService = learningResourcesService;
            _mapper = mapper;
        }
        
        
        [HttpGet("/dict/{dictName}", Name = nameof(GetWordDictionaryByName))]
        public async Task<ActionResult<ReadWordDictionaryDto>> GetWordDictionaryByName(
                [FromRoute] string dictName,
                CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameSpec(dictName);
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

            var createdItemResult = await _learningResourcesService.CreateAsync(newWordDictionary, cancellationToken);

            if (!createdItemResult.IsSuccess)
            {
                // TODO: Error behaviour
            }
            
            var response = _mapper.Map<ReadWordDictionaryDto>(createdItemResult.Value);

            return CreatedAtRoute(
                nameof(GetWordDictionaryByName), 
                new { dictName = response.Name }, 
                response
            );
        }

        [HttpPut("/dict/{dictName}", Name = nameof(UpdateWordDictionaryByName))]
        public async Task<ActionResult> UpdateWordDictionaryByName(
            [FromRoute] string dictName,
            [FromBody] UpdateWordDictionaryDto dto,
            CancellationToken cancellationToken = new())
        {
            var result = await _learningResourcesService.UpdateInformationByNameAsync(dictName, dto, cancellationToken);

            if (result.IsSuccess)
            {
                var response = _mapper.Map<ReadWordDictionaryDto>(result.Value);
                return Ok(response);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(dictName);
            }

            if (result.Status == ResultStatus.Error)
            {
                return BadRequest(result.Errors);
            }

            return BadRequest();
        }
    }
}