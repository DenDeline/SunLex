using Ardalis.Specification.EntityFrameworkCore;
using SunLex.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunLex.Infrastructure.Data
{
    public class EfRepository<T>: RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T: class, IAggregateRoot
    {
        public EfRepository(AppDbContext context): base(context)
        {

        }
    }
}
