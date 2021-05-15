using Ardalis.Specification;

namespace SunLex.SharedKernel.Interfaces
{
    public interface IReadRepository<T>: IReadRepositoryBase<T> where T: class, IAggregateRoot
    {
        
    }
}
