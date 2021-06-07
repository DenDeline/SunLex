using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using SunLex.ApplicationCore.WordDictionaryAggregate;
using SunLex.SharedKernel.Dtos.WordDictionary;

namespace SunLex.ApplicationCore.Interfaces
{
    public interface ILearningResourcesService
    { 
        Task<Result<WordDictionary>> CreateAsync(
            WordDictionary wordDictionary,
            CancellationToken cancellationToken = new());

        Task<Result<WordDictionary>> UpdateInformationByNameAsync(
            string dictionaryName, 
            UpdateWordDictionaryDto dto,
            CancellationToken cancellationToken = new()
        );
    }
}