using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CardPortfolio.Data;
using CardPortfolio.Models;
using CardPortfolio.Models.Enums;
using CardPortfolio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CardPortfolio.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IBlogPostData _blogPostData;
        public BlogPostsController( IHostingEnvironment hostingEnvironment, IHtmlHelper htmlHelper, IBlogPostData blogPostData)
        {
            _hostingEnvironment = hostingEnvironment;
            _htmlHelper = htmlHelper;
            _blogPostData = blogPostData;
        }
        public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber, string categoryName)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewBag.PageHeader = "All Posts";
            if(searchString != null)
            {
                pageNumber = 1;

            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var posts = _blogPostData.GetAllBlogPosts();
            if (!String.IsNullOrEmpty(searchString))
            {
                
                posts = posts.Where(s => s.Title.Contains(searchString) || s.Abstract.Contains(searchString));
                if(posts.Count() == 0)
                {
                    ViewBag.PageHeader = "No Posts Found";
                }
                else
                {
                    ViewBag.PageHeader = "Posts containing " + '"' + searchString + '"';
                }
            }
            ViewBag.ThreeLabel = "Recent Posts";
            ViewBag.RecentPosts = _blogPostData.GetThreeRecentPosts();
            // filter by category name
            if (!String.IsNullOrEmpty(categoryName))
            {
                switch(categoryName)
                {
                    case "Credit Cards":
                        ViewBag.PageHeader = "Credit Cards";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.CreditCards).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.CreditCards);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Cash Back":
                        ViewBag.PageHeader = "Cash Back Cards";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.cashback).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.cashback);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Cash Advance":
                        ViewBag.PageHeader = "Cash Advance Cards";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.cashadvance).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.cashadvance);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Balance Transfer":
                        ViewBag.PageHeader = "Balance Transfer Cards";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.balancetransfer).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.balancetransfer);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Intro Offer":
                        ViewBag.PageHeader = "Intro Offers";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.introoffer).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPOsts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.introoffer);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Sign Up Bonus":
                        ViewBag.PageHeader = "Sign Up Bonuses";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.signupbonus).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.signupbonus);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Travel Cards":
                        ViewBag.PageHeader = "Travel Cards";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.travelcards).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.travelcards);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Savings Account":
                        ViewBag.PageHeader = "Savings Accounts";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.SavingsAccounts).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.SavingsAccounts);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Checking Accounts":
                        ViewBag.PageHeader = "Checking Accounts";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.CheckingsAccounts).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.CheckingsAccounts);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Certificate Accounts":
                        ViewBag.PageHeader = "Certificate Accounts";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.Cds).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.Cds);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Money Market Accounts":
                        ViewBag.PageHeader = "Money Market Accounts";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.MoneyMarketAccounts).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.MoneyMarketAccounts);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Mortgages":
                        ViewBag.PageHeader = "Mortgages";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.Mortgages).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.Mortgages);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Auto Loans":
                        ViewBag.PageHeader = "Auto Loans";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.AutoLoans).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.AutoLoans);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Home Equity Loans":
                        ViewBag.PageHeader = "Home Equity Loans";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.HomeEquityLoans).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.HomeEquityLoans);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Home Equity Lines of Credit":
                        ViewBag.PageHeader = "Home Equity Lines of Credit";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.HomeEquityLinesOfCredit).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.HomeEquityLinesOfCredit);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Unsecured Personal Loans":
                        ViewBag.PageHeader = "Unsecured Personal Loans";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.UnsecuredPersonalLoans).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.UnsecuredPersonalLoans);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Secured Personal Loans":
                        ViewBag.PageHeader = "Secured Personal Loans";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.SecuredPersonalLoans).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.SecuredPersonalLoans);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Secured Lines of Credit":
                        ViewBag.PageHeader = "Secured Lines of Credit";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.SecuredLinesOfCredit).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.SecuredLinesOfCredit);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;
                    case "Unsecured Lines of Credit":
                        ViewBag.PageHeader = "Unsecured Lines of Credit";
                        posts = posts.Where(p => p.BlogPostCategory == BlogPostCategory.UnsecuredLinesOfCredit).OrderByDescending(p => p.CreationDate);
                        ViewBag.RecentPosts = _blogPostData.GetThreeCategoryPosts(BlogPostCategory.UnsecuredLinesOfCredit);
                        ViewBag.ThreeLabel = "Similar Posts";
                        break;



                }
               
               
            }

            
            int pageSize = 3;
            return View(await PaginatedList<BlogPost>.CreateAsync(posts.AsNoTracking(), pageNumber ?? 1, pageSize));
            
        }

        // GET: BlogPosts/Create
        [Authorize(Roles="Administrator")]
        public IActionResult Create()
        {
            ViewBag.BlogPostCategoryList = _htmlHelper.GetEnumSelectList<BlogPostCategory>();
            
            return View();
        }

        // POST: BlogPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult Create(string title, string Abstract,
           string BodyText, BlogPostCategory blogPostCategory, BlogPost blogPost )
        {
            if (ModelState.IsValid) 
            {
                blogPost.Title = title;
                blogPost.Abstract = Abstract;
                blogPost.BodyText = BodyText;
                blogPost.CreationDate = DateTime.Now;
                blogPost.BlogPostCategory = blogPostCategory;
                var addStatus = _blogPostData.AddBlogPost(blogPost);
                if(addStatus == 0)
                {
                    var commitStatus = _blogPostData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = blogPost.Id });
                    }
                    else
                    {
                        // replace with logging message and redirect
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Replace with logging message and redirect
                    return RedirectToAction("Index");
                }
               
            }
            return RedirectToAction("Index");

        }

        // GET: BlogPosts/Edit/3
        [Authorize(Roles ="Administrator")]
        public IActionResult Edit(int id)
        {
            var post = _blogPostData.GetBlogPostById(id);
            if(post != null)
            {
                ViewBag.BlogPostCategoryList = _htmlHelper.GetEnumSelectList<BlogPostCategory>();
                TempData["BlogPostId"] = post.Id;
                return View(post);
            }
            return RedirectToAction("Index");
        }

        // POST: BlogPosts/Edit/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult Edit(string title, string Abstract, BlogPostCategory blogPostCategory, string bodyText, bool isPublished)
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["BlogPostId"];
                var post = _blogPostData.GetBlogPostById(id);
                if (post != null)
                {
                    post.Title = title;
                    post.UpdatedDate = DateTime.Now;
                    post.Abstract = Abstract;
                    post.BodyText = bodyText;
                    post.IsPublished = isPublished;
                    post.BlogPostCategory = blogPostCategory;
                    var status = _blogPostData.Commit();
                    if(status == 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // add error loggin message here
                    }
                }
                
            }
            return View();
            
        }
        

        // GET: BlogPosts/Details/3
        public IActionResult Details(int id)
        {
            var post = _blogPostData.GetBlogPostById(id);

            if(post != null)
            {
                ViewBag.SimilarPosts = _blogPostData.GetThreeCategoryPosts(post.BlogPostCategory, id);
                if (post.IsPublished)
                {
                    return View(post);
                }
                else
                {
                    if(User.IsInRole("Administrator") && post.IsPublished == false)
                    {
                        return View(post);

                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Error404", "Error", null);
        }

        // GET: BlogPosts/Delete/3
        [Authorize(Roles ="Administrator")]
        public IActionResult Delete(int id)
        {
            var post = _blogPostData.GetBlogPostById(id);
            if (post != null)
            {
                TempData["BlogPostId"] = post.Id;
                return View(post);
            }
            return RedirectToAction("Index");
        }

        // POST: BlogPosts/Delete/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                int postId = (int)TempData["BlogPostId"];
                var post = _blogPostData.GetBlogPostById(postId);
                if (post != null)
                {
                    var removeStatus = _blogPostData.RemoveBlogPost(post);
                    if(removeStatus == 0)
                    {
                        var commitStatus = _blogPostData.Commit();
                        if(commitStatus == 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // log message here
                            
                        }
                    }
                    
                   
                }
            }
            return View();

        }

        // GET: HomeEquityLinesOfCredit/AddImage/3
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["BlogPostId"] = id;
            return View();

        }

        // POST: BlogPosts/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddBlogPostImageViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                int blogId = (int)TempData["BlogPostId"];
                // TODO: Refactor
                var post = _blogPostData.GetBlogPostById(blogId);
                if (post != null)
                {
                    post.ImageUrl = uniqueFileName;
                    var commitStatus = _blogPostData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = (int)TempData["BlogPostId"] });
                    }
                    else
                    {
                        // log message
                    }
                }
                
            }
            return RedirectToAction("Index");
        }

        // GET: BlogPosts/UnpublishedPosts
        [Authorize(Roles="Administrator")]
        public IActionResult UnpublishedPosts()
        {
            var list = _blogPostData.GetUnpublishedPosts();
            return View(list);
        }

        // GET: BlogPosts/Publish/5
        [Authorize(Roles="Administrator")]
        public IActionResult Publish(int id)
        {
            var post = _blogPostData.GetBlogPostById(id);
            if(post != null)
            {
                TempData["PostId"] = id;
                return View(post);
            }
            else
            {
                // Post not found
                return RedirectToAction("Index");
            }
        }

        // POST: BlogPosts/Publish/5
        [Authorize(Roles="Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Publish()
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["PostId"];
                var post = _blogPostData.GetBlogPostById(id);
                if (post != null)
                {
                    post.IsPublished = true;
                    var commitStatus = _blogPostData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", "BlogPosts", new { id = post.Id });
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // post is null
                    return RedirectToAction("Index");
                }
            }
            // Model State not valid
            return View();
        }

        // GET: BlogPOsts/PublishedPosts
        [Authorize(Roles="Administrator")]
        public IActionResult PublishedPosts()
        {
            var posts = _blogPostData.GetAllBlogPosts();
            return View(posts);
        }

        // GET: BlogPosts/Unpublish/5
        [Authorize(Roles="Administrator")]
        public IActionResult Unpublish(int id)
        {
            var post = _blogPostData.GetBlogPostById(id);
            if(post != null)
            {
                TempData["PostId"] = id;
                return View(post);
            }
            else
            {
                // Post was null
                return RedirectToAction("Index");
            }
        }

        // POST: BlogPosts/Unpublish/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrator")]
        public IActionResult Unpublish()
        {
            var id = (int)TempData["PostId"];
            var post = _blogPostData.GetBlogPostById(id);
            if(post != null)
            {
                post.IsPublished = false;
                var commitStatus = _blogPostData.Commit();
                if(commitStatus == 0)
                {
                    // commit succeeded
                    return RedirectToAction("Details", "BlogPosts", new { id = post.Id});

                }
                else
                {
                    // commit failed
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // post is null
                return RedirectToAction("Index");
            }
        }
    }
}