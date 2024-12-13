using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_datas")]
    public class DatasEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_Title { get; set; }
        public string? F_Description { get; set; }
        public string? F_Content { get; set; }
        /// <summary>
        /// 内容类型
        /// 0:moment;
        /// 1:article;
        /// 2:video;
        /// 3:audio;
        /// 4:website;
        /// </summary>
        public int? F_Type { get; set; }
        public string? F_Url { get; set; }
        public string? F_CoverImg { get; set; }        
        public string? F_Imgs {  get; set; }
        public string? F_VideoUrl { get; set; }
        public string? F_IFrameCode { get; set; }
        public string? F_AudioUrl { get; set; }

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
        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
}
