using CardPortfolio.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CardPortfolio.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string BodyText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublished { get; set; }
        public BlogPostCategory BlogPostCategory { get; set; }
        

        

    }
}
