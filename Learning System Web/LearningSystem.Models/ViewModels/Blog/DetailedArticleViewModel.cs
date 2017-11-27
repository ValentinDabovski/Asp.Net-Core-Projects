namespace LearningSystem.Models.ViewModels.Blog
{
    using System;
    using AutoMapper;
    using Common.Mapping;
    using DataModels;

    public class DetailedArticleViewModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string AuthorName { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Article, DetailedArticleViewModel>()
                .ForMember(viewModel => viewModel.AuthorName, cfg => cfg.MapFrom(a => a.Author.Name));
        }
    }
}
