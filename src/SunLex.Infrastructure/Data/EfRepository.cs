using Ardalis.Specification.EntityFrameworkCore;
using SunLex.SharedKernel.Interfaces;

namespace SunLex.Infrastructure.Data
{
    public class EfRepository<T>: RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T: class, IAggregateRoot
    {
        public EfRepository(AppDbContext context): base(context)
        {

        }
    }
}
