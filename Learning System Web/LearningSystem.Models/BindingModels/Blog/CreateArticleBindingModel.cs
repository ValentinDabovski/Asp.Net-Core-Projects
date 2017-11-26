namespace LearningSystem.Models.BindingModels.Blog
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstats;

    public class CreateArticleBindingModel
    {
        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        [MinLength(ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(6000)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
        
    }
}
