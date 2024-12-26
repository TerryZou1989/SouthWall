using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;

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
        public async Task<DataTable> QuerySql(string sql,params MySqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            // 使用 ADO.NET 直接执行 SQL 查询
            using (var connection = this.Database.GetDbConnection())
            {
                connection.Open(); // 打开连接
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.Parameters.AddRange(parameters);
                    using (var reader =await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader); // 将结果加载到 DataTable
                    }
                }
            }
            return dataTable;
        }
        public DbSet<DatasEntity> Datas { get; set; }
        public DbSet<RequestLogsEntity> RequestLogs { get; set; }
        public DbSet<TimesEntity> Times { get; set; }
        public DbSet<VideosEntity> Videos { get; set; }
        public DbSet<AudiosEntity> Audios { get; set; }
        public DbSet<ArticlesEntity> Articles { get; set; }
        public DbSet<MessagesEntity> Messages { get; set; }
        public DbSet<WebSitesEntity> WebSites { get; set; }
        public DbSet<ShiJusEntity> ShiJus { get; set; }
        public DbSet<TouXiangsEntity> TouXiangs { get; set; }
    }
}
