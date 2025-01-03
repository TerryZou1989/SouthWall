﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SouthWall
{
    [Table("t_articles")]
    public class ArticlesEntity : EntityBase
    {
        [Key]
        public string F_Id { get; set; }
        public string? F_Title { get; set; }
        public string? F_CoverImg { get; set; }
        public string? F_Content { get; set; } 
        public string? F_ArticleUrl {  get; set; }

        public override void InitCreate()
        {
            F_Id = Guid.NewGuid().ToString();
            base.InitCreate();
        }
    }
}
