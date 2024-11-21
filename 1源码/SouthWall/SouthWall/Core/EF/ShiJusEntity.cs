using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_shijus")]
    public class ShiJusEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_P { get; set; }
        public string? F_N { get; set; }

        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
}
