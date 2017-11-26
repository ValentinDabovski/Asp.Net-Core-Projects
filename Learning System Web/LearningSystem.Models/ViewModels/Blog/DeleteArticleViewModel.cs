namespace LearningSystem.Models.ViewModels.Blog
{
    using Common.Mapping;
    using DataModels;
    public class DeleteArticleViewModel : IMapFrom<Article>
    {
        public int  Id { get; set; }

        public string Title { get; set; }
    }
}
