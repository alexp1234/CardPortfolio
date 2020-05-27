using CardPortfolio.Data;
using CardPortfolio.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IBlogPostData
    {

        int AddBlogPost(BlogPost blogPost);

        int Commit();

        BlogPost GetBlogPostById(int id);

        int RemoveBlogPost(BlogPost blogPost);

        IQueryable<BlogPost> GetAllBlogPosts();
        IEnumerable<BlogPost> GetUnpublishedPosts();
        IEnumerable<BlogPost> GetThreeRecentPosts();

        IEnumerable<BlogPost> GetThreeCategoryPosts(BlogPostCategory blogPostCategory);

    }
}
