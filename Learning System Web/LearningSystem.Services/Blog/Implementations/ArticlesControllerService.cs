namespace LearningSystem.Services.Blog.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Inetrfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.BindingModels.Blog;
    using Models.DataModels;
    using Models.ViewModels.Blog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static Models.DataConstats;

    public class ArticlesControllerService : IArticlesControllerService
    {
        private readonly LearningSystemDbContext db;
        private readonly UserManager<User> userManager;

        public ArticlesControllerService(LearningSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;

        }


        public async Task CreateArticleAsync(CreateArticleBindingModel articleModel, string authorId)
        {
            var author = await this.userManager.FindByIdAsync(authorId);

            this.db.Articles.Add(new Article()
            {
                Title = articleModel.Title,
                PublishDate = DateTime.UtcNow,
                Content = articleModel.Content,
                AuthorId = authorId,
                Author = author
            });
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllArticlesViewModel>> AllArticlesAsync(int page = 1)
        {
            var allArticles = await
                this
                .db
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * ArticlePageSize)
                .Take(ArticlePageSize)
                .ProjectTo<AllArticlesViewModel>()
                .ToListAsync();

            return allArticles;
        }

        public async Task<EditArticleBindngModel> GetArticleToEditAsync(int id)
        {
            var articleExists = await this.ArticleExistsAsync(id);

            if (!articleExists)
            {
                return null;
            }

            return await this.db
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<EditArticleBindngModel>()
                .FirstOrDefaultAsync();
        }

        public async Task EditArticleAsync(EditArticleBindngModel editModel)
        {
            var articleExists = await this.ArticleExistsAsync(editModel.Id);

            if (articleExists)
            {
                var articleToEdit = await this.db.Articles.FindAsync(editModel.Id);
                articleToEdit.Content = editModel.Content;
                articleToEdit.Title = editModel.Title;

                await this.db.SaveChangesAsync();
            }

        }

        public async Task<DeleteArticleViewModel> GetArticleToDeleteAsync(int id)
        {
            var articleExists = await this.ArticleExistsAsync(id);

            if (!articleExists)
            {
                return null;
            }

            return await this.db
                .Articles
                .Where(a => a.Id == id).ProjectTo<DeleteArticleViewModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DestroySuccessfullAsync(DeleteArticleViewModel deleteModel)
        {
            var articleExists = await this.ArticleExistsAsync(deleteModel.Id);

            if (articleExists)
            {
                var articleToDelite = await this.db.Articles.FindAsync(deleteModel.Id);

                this.db.Articles.Remove(articleToDelite);
                await this.db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> TotalAsync()
        {
            return await this.db.Articles.CountAsync();
        }

        public async Task<DetailedArticleViewModel> DetailedArticleAsync(int id)
        {
            var articleExists = await this.ArticleExistsAsync(id);

            if (!articleExists)
            {
                return null;
            }

            return await this.db
                 .Articles
                 .Where(a => a.Id == id)
                 .ProjectTo<DetailedArticleViewModel>()
                 .FirstOrDefaultAsync();

        }


        private async Task<bool> ArticleExistsAsync(int id)
        {
            return await this.db.Articles.AnyAsync(a => a.Id == id);
        }


    }
}
