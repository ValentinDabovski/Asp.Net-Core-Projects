namespace LearningSystem.Models.ViewModels.Blog
{
    using AutoMapper;
    using Common.Mapping;
    using DataModels;

    public class AllArticlesViewModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int  Id { get; set; }

        public string Title { get; set; }

        public string AuthorName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Article, AllArticlesViewModel>()
                .ForMember(a => a.AuthorName, cfg => cfg.MapFrom(ar => ar.Author.Name));
        }
    }
}
