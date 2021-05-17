using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace SunLex.WebAPI.Endpoints.Dictionary
{
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateWordDictionaryRequest>
        .WithResponse<CreateWordDictionaryResponse>
    {
        private readonly IRepository<WordDictionary> _repository;

        public Create(IRepository<WordDictionary> repository)
        {
            _repository = repository;
        }

        [HttpPost("/Dictionaries")]
        public override async Task<ActionResult<CreateWordDictionaryResponse>> HandleAsync(
            CreateWordDictionaryRequest request, 
            CancellationToken cancellationToken = default)
        {
            var newWordDictionary = new WordDictionary(request.Name);
            var createdItem = await _repository.AddAsync(newWordDictionary, cancellationToken);

            var response = new CreateWordDictionaryResponse
            {
                Id = createdItem.Id,
                Name = createdItem.Name
            };

            return CreatedAtRoute(
                "GetDictionaryById", 
                new GetDictionaryByIdRequest { DictionaryId = response.Id }, 
                response
                );
        }
    }
}
