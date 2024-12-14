using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    public interface IMDBAccess
    {
        Task AddDBPartition(string tableName, string partitionName, string lessValue);
        Task<List<MySqlPartitionEntity>> QueryDBPartitions(string tableName);
    }
    public class MDBAccess : DBAccessBase, IMDBAccess
    {
        public MDBAccess(SWDbContext context) : base(context) { }

        public async Task AddDBPartition(string tableName, string partitionName, string lessValue)
        {
            string sql = $"ALTER TABLE {tableName} ADD PARTITION (PARTITION {partitionName} VALUES LESS THAN ({lessValue}));";
            int row = await this._context.Database.ExecuteSqlRawAsync(sql);
        }

        public Task<List<MySqlPartitionEntity>> QueryDBPartitions(string tableName)
        {
            FormattableString sql = $"SELECT * FROM INFORMATION_SCHEMA.partitions WHERE TABLE_SCHEMA = schema() AND TABLE_NAME = {tableName};";
            return this._context.Database.SqlQuery<MySqlPartitionEntity>(sql).ToListAsync();
        }
    }
    public class MySqlPartitionEntity
    {
        public string Table_Name { get; set; }
        public string Partition_Name { get; set; }
        public string Partition_Description { get; set; }
        public long? Table_Rows { get; set; }
        public long? Avg_Row_Length { get; set; }
        public long? Data_Length { get; set; }
        public long? Index_Length { get; set; }
        public DateTime? Create_Time { get; set; }
        public DateTime? Update_Time { get; set; }
    }
}
