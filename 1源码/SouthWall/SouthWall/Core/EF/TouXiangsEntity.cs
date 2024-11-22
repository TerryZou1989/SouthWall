using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_touxiangs")]
    public class TouXiangsEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_ImgSrc { get; set; }

        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
}
