using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_messages")]
    public class MessagesEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_Content { get; set; } 
        public string? F_UserName {  get; set; }

        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
}
