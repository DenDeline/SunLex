using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunLex.SharedKernel.Interfaces
{
    public interface IRepository<T>: IReadRepository<T>, IRepositoryBase<T> where T: class, IAggregateRoot
    {
        
    }
}
