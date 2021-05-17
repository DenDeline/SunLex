using System.Linq;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.ApplicationCore.WordDictionaryAggregate.Specifications;
using SunLex.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SunLex.WebAPI.Endpoints.Dictionary
{

    public class GetById : BaseAsyncEndpoint
        .WithRequest<GetDictionaryByIdRequest>
        .WithResponse<GetDictionaryByIdResponse>
    {
        private readonly IReadRepository<WordDictionary> _repository;

        public GetById(IReadRepository<WordDictionary> repository)
        {
            _repository = repository;
        }


        [HttpGet("/Dictionaries/{DictionaryId:guid}", Name = "GetDictionaryById")]
        public override async Task<ActionResult<GetDictionaryByIdResponse>> HandleAsync(
            [FromRoute] GetDictionaryByIdRequest request, 
            CancellationToken cancellationToken = default)
        {
            var spec = new DictionaryByIdWithWordsSpec(request.DictionaryId);
            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (entity == null) return NotFound();

            var response = new GetDictionaryByIdResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                WordsTranslations = entity.WordsTranslations.Select(t => new WordTranslationDto
                {
                    Id = t.Id,
                    FromWord = t.FromWord.ToString(),
                    ToWord = t.ToWord.ToString(),
                }).ToList()
            };

            return Ok(response);
        }
    }
}
