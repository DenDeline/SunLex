using Ardalis.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using SunLex.ApplicationCore.WordDictionaryAggregate;

namespace SunLex.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base (options)
        {

        }

        public DbSet<WordDictionary> WordDictionaries { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
        }
    }
}
