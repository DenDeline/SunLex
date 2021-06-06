using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using SunLex.ApplicationCore.Interfaces;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.ApplicationCore.WordDictionaryAggregate.Specifications;
using SunLex.SharedKernel.Interfaces;

namespace SunLex.ApplicationCore.Services
{
    public class LearningResourcesService: ILearningResourcesService
    {
        private readonly IRepository<WordDictionary> _repository;

        public LearningResourcesService(IRepository<WordDictionary> repository)
        {
            _repository = repository;
        }

        public async Task<Result<WordDictionary>> UpdateInformationByNameAsync(
            string dictionaryName,
            string newDictionaryName, 
            string? newDescription, 
            string? newThumbnailImageUrl,
            CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameSpec(dictionaryName);
            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (entity is null) return Result<WordDictionary>.NotFound();

            try
            {
                entity.UpdateName(newDictionaryName.Trim());
                entity.UpdateDescription(newDescription?.Trim());
                entity.UpdateThumbnailImageUrl(newThumbnailImageUrl?.Trim());
                
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            catch (Exception e)
            {
                return Result<WordDictionary>.Error(e.Message);
            }
            
            return Result<WordDictionary>.Success(entity);
        }
    }
}