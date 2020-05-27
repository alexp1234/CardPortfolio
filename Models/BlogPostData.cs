using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using CardPortfolio.Models.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class BlogPostData : IBlogPostData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public BlogPostData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddBlogPost(BlogPost blogPost)
        {
            try
            {
                _db.BlogPosts.Add(blogPost);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public int Commit()
        {
            try
            {
                _db.SaveChanges();
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }


        public IQueryable<BlogPost> GetAllBlogPosts()
        {
            var posts = from p in _db.BlogPosts where p.IsPublished == true select p;
            return posts.OrderByDescending(p => p.CreationDate);
        }

        public BlogPost GetBlogPostById(int id)
        {
           return _db.BlogPosts.Find(id);
           
        }

        public IEnumerable<BlogPost> GetThreeCategoryPosts(BlogPostCategory blogPostCategory)
        {
            return _db.BlogPosts.Where(p => p.BlogPostCategory == blogPostCategory && p.IsPublished).OrderByDescending(p => p.CreationDate).Take(3);
        }

        public IEnumerable<BlogPost> GetThreeRecentPosts()
        {
            var posts = _db.BlogPosts.Where(p => p.IsPublished).OrderByDescending(p => p.CreationDate).Take(3).ToList();
            return posts;
        }

        public IEnumerable<BlogPost> GetUnpublishedPosts()
        {
            return _db.BlogPosts.Where(b => b.IsPublished == false).ToList();
        }

        public int RemoveBlogPost(BlogPost blogPost)
        {
            try
            {
                _db.BlogPosts.Remove(blogPost);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }
    }
}
