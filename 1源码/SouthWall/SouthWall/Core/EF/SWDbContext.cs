using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace SouthWall
{
    public class SWDbContext : DbContext
    {
        string _ConnectionString = string.Empty;
        public SWDbContext(DbContextOptions<SWDbContext> options) : base(options) { 
        }
        //public OMADbContext(string connStr)
        //{
        //    _ConnectionString = connStr;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //   base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseMySql(_ConnectionString, new MySqlServerVersion(new Version()));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public IDbContextTransaction BeginTransaction()
        {
            return this.Database.BeginTransaction();
        }

        public DbSet<TimesEntity> Times { get; set; }
        public DbSet<VideosEntity> Videos { get; set; }
    }
}
