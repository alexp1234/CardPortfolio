using CardPortfolio.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.ViewModels
{
    public class AddBlogPostImageViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string BodyText { get; set; }
        public IFormFile Image { get; set; }
        public bool IsPublished { get; set; }
        public BlogPostCategory blogPostCategory { get; set; }
    }
}
