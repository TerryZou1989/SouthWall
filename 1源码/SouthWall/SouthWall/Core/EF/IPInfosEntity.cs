using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_ipinfos")]
    public class IPInfosEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_IP { get; set; }
        public string? F_Country { get; set; }
        public string? F_Province { get; set; }
        public string? F_City { get; set; }
        public string? F_District {  get; set; }
        public string? F_Lat {  get; set; }
        public string? F_Lon {  get; set; }       
        public override void InitCreate()
        {  
            base.InitCreate();
            F_Id = Guid.NewGuid().ToString();       
        }
    }
}
