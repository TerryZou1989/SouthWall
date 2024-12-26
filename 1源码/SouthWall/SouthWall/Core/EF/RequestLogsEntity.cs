using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_requestlogs")]
    public class RequestLogsEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_IP { get; set; }
        public string? F_Country { get; set; }
        public string? F_Province { get; set; }
        public string? F_City { get; set; }
        public string? F_Lat {  get; set; }
        public string? F_Lon {  get; set; }
        public string? F_Url { get; set; }    
        public string? F_Method {  get; set; }
        public string? F_UserAgent {  get; set; }
        public int? F_StatusCode { get; set; }
        /// <summary>
        /// 页面
        /// index;
        /// times;
        /// articles;
        /// article;
        /// videos;
        /// audios;
        /// websites;
        /// owner;
        /// messages;
        /// </summary>
        public string? F_Module { get; set; }
        public string? F_DataId { get; set; }
        public int F_PartitionKey {  get; set; }
        public override void InitCreate()
        {  
            base.InitCreate();
            F_Id = Guid.NewGuid().ToString();
            F_PartitionKey=this.F_CreateTime.Value.ToString("yyyyMMdd").ToInt32();          
        }
    }

    public class RequestAndIPCountEntity
    {
        public int F_PartitionKey { get; set; }
        public int F_RequestCount { get; set; }
        public int F_IPCount { get; set; }
    }
    public class IPRequestCountEntity
    {
        public string? F_IP { get; set; }
        public string? F_Country { get; set; }
        public string? F_Province { get; set; }
        public string? F_City { get; set; }
        public int? F_RequestCount { get; set; }
    }
    public class CountryRequestCountEntity
    {
        public string? F_Country { get; set; }
        public int F_RequestTotal { get; set; }
        public int F_DailyRequestCount { get; set; }
    }
    public class ProvinceRequestCountEntity
    {
        public string? F_Province { get; set; }
        public int F_RequestTotal { get; set; }
        public int F_DailyRequestCount { get; set; }
    }
}
