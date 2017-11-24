namespace LearningSystem.Models.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstats;

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ArticleTitleMaxLength)]
        [MinLength(ArticleTitleMinLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(6000)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
