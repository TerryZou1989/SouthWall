using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_audios")]
    public class AudiosEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_Title { get; set; }
        public string? F_CoverImg { get; set; }
        public string? F_Description { get; set; }
        public string? F_AudioUrl { get; set; }

        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
}
