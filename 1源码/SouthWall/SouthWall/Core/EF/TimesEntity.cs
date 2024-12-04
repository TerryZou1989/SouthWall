using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_times")]
    public class TimesEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_Title { get; set; }
        public string? F_Url { get; set; }
        public string? F_Content { get; set; }
        public string? F_Imgs { get; set; }
        public string? F_Video { get; set; }
        [NotMapped]
        public List<ImgSrc> E_ImgSrcList
        {
            get
            {
                if (string.IsNullOrEmpty(F_Imgs))
                {
                    return new List<ImgSrc>();
                }
                return F_Imgs.ToObject<List<ImgSrc>>();
            }
        }
        [NotMapped]
        public string? E_SearchKey { get; set; }

        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
    public class ImgSrc
    {
        public string imgId { get; set; }
        public string imgSrc { get; set; }
    }
}
