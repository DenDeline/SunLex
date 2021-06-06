using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using SunLex.ApplicationCore.WordDictionaryAggregate;

namespace SunLex.ApplicationCore.Interfaces
{
    public interface IWordDictionaryService
    {
        Task<Result<WordDictionary>> UpdateInformationByNameAsync(
            string dictionaryName, 
            string newDictionaryName, 
            string? newDescription, 
            string? newThumbnailImageUrl,
            CancellationToken cancellationToken = new()
        );
    }
}