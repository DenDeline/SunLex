using Ardalis.Specification;

namespace SunLex.SharedKernel.Interfaces
{
    public interface IRepository<T>: IRepositoryBase<T> where T: class, IAggregateRoot
    {
        
    }
}
