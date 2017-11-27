using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Models.ViewModels.Blog
{
    using static DataConstats;
    public class ArticleListingViewModel
    {
        
        public IEnumerable<AllArticlesViewModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalArticles / ArticlePageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

    }
}
