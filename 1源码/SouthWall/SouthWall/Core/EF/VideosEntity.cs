using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_videos")]
    public class VideosEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_Title { get; set; }
        public string? F_ConverImg { get; set; }
        public string? F_VideoUrl { get; set; }
        public string? F_VideoCode {  get; set; }

        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
}
