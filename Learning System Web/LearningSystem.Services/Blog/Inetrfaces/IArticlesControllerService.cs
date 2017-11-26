namespace LearningSystem.Services.Blog.Inetrfaces
{
    using Models.BindingModels.Blog;
    using Models.ViewModels.Blog;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticlesControllerService
    {
        Task CreateArticleAsync(CreateArticleBindingModel articleModel, string authorId);

        Task<IEnumerable<AllArticlesViewModel>> AllArticlesAsync();

        Task<EditArticleBindngModel> GetArticleToEditAsync(int id);

        Task EditArticleAsync(EditArticleBindngModel editModel);

        Task<DeleteArticleViewModel> GetArticleToDeleteAsync(int id);

        Task<bool> DestroySuccessfullAsync(DeleteArticleViewModel deleteModel);

    }
}
