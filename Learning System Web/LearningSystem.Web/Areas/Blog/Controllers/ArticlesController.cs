using Microsoft.AspNetCore.Authorization;

namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Threading.Tasks;
    using Models.BindingModels.Blog;
    using Models.DataModels;
    using Services.Blog.Inetrfaces;
    using Web.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels.Blog;
    using Services.Html;

    using static Common.Constants.WebConstants;

    [Area(BlogArea)]
    [AuthorizeRoles(AdministratorRole, BlogAuthorRole)]
    public class ArticlesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IArticlesControllerService service;
        private readonly IHtmlService htmlService;

        public ArticlesController(UserManager<User> userManager, IArticlesControllerService service, IHtmlService htmlService)
        {
            this.userManager = userManager;
            this.service = service;
            this.htmlService = htmlService;
        }

        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All(int page = 1)
        {
            var viewModels = await this.service.AllArticlesAsync(page);
            return View(new ArticleListingViewModel()
            {
                Articles = viewModels,
                TotalArticles = await this.service.TotalAsync(),
                CurrentPage = page,

            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleBindingModel articleModel)
        {
            var author = await this.userManager.GetUserAsync(this.User);
            var userInBlogAuthorRole = await this.userManager.IsInRoleAsync(author, BlogAuthorRole);

            articleModel.Content = this.htmlService.Sanitize(articleModel.Content);

            if (!ModelState.IsValid)
            {
                return View(articleModel);
            }

            if (!userInBlogAuthorRole)
            {
                TempData.AddErrorMessage("You are not authorized for this action.");
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
            }

            await this.service.CreateArticleAsync(articleModel, author.Id);

            TempData.AddSuccessMessage("Succsessfully posted article.");
            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await this.service.DetailedArticleAsync(id);

            if (viewModel == null)
            {
                TempData.AddErrorMessage("No such article.");
                return RedirectToAction(nameof(All));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.service.GetArticleToEditAsync(id);

            if (viewModel == null)
            {
                TempData.AddErrorMessage("No such article.");
                return RedirectToAction(nameof(All));
            };


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditArticleBindngModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editModel);
            }


            await this.service.EditArticleAsync(editModel);

            TempData.AddSuccessMessage($"Successfully edited article");

            return RedirectToAction(nameof(All));

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await this.service.GetArticleToDeleteAsync(id);

            if (viewModel == null)
            {
                TempData.AddErrorMessage("No such article.");
                return RedirectToAction(nameof(All));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Destroy(DeleteArticleViewModel articleModel)
        {
            var destroySuccessfull = await this.service.DestroySuccessfullAsync(articleModel);

            if (!destroySuccessfull)
            {
                TempData.AddErrorMessage("Article cannot be deleted.");
                RedirectToAction(nameof(All));
            }
            TempData.AddSuccessMessage($"Article deleted.");

            return RedirectToAction(nameof(All));
        }
    }
}