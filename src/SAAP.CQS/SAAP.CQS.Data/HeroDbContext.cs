using Microsoft.EntityFrameworkCore;

namespace SAAP.CQS.Data
{
    public sealed class HeroDbContext : DbContext
    {
        #region Public Constructors

        public HeroDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "heroes.db");
        }

        #endregion

        #region Public Properties

        public string DbPath { get; init; }
        public DbSet<Hero> Heroes { get; set; } = null!;

        #endregion

        #region Protected Methods

        // The following configures EF to create a Sqlite database file in the special "local"
        // folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        #endregion
    }
}