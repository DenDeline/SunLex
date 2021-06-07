using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using SunLex.ApplicationCore.Interfaces;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.ApplicationCore.WordDictionaryAggregate.Specifications;
using SunLex.SharedKernel.Dtos.WordDictionary;
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

        public async Task<Result<WordDictionary>> CreateAsync(
            WordDictionary wordDictionary,
            CancellationToken cancellationToken = new())
        {
            return await _repository.AddAsync(wordDictionary, cancellationToken);
        }

        public async Task<Result<WordDictionary>> UpdateInformationByNameAsync(
            string dictionaryName,
            UpdateWordDictionaryDto dto,
            CancellationToken cancellationToken = new())
        {
            var spec = new DictionaryByNameSpec(dictionaryName);
            var entity = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (entity is null) return Result<WordDictionary>.NotFound();

            try
            {
                entity.UpdateName(dto.Name.Trim());
                entity.UpdateDescription(dto.Description?.Trim());
                entity.UpdateThumbnailImageUrl(dto.ThumbnailImageUrl?.Trim());
                
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