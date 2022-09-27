using AgorusApi.Context.Helper;
using AgorusApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AgorusApi.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public DbSet<FileHistoryModel> FileHistories { get; set; }

        public string DbPath { get; }

        public AppDbContext(IOptions<DbConfigOptions> dbConfigOptions, ILogger<AppDbContext> logger)
        {
            var dbFileName = string.IsNullOrEmpty(dbConfigOptions.Value.FileName) ? "Agorus.db" : dbConfigOptions.Value.FileName;

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "Agorus");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DbPath = Path.Join(path, dbFileName);
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            options.UseSqlite($"Data Source={DbPath}");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileModel>()
                .HasMany(e => e.FileHistoryModels)
                .WithOne(e => e.FileModel)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
