using Microsoft.AspNetCore.Mvc;
using myBlog.Data.Repository.FileManager;
using myBlog.Models;
using myBlog.Models.Comments;
using myBlog.Models.ViewModels;
using myBlog.Repository.IRepository;

namespace myBlog.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFileManager _fileManager;

        public PostController(IRepository repo, 
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index(int pageNumber, string category, string search, string orderBy)
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });
            var posts = _repo.GetAllPosts(pageNumber, category, search, orderBy);
            return View(posts);
        }

        public IActionResult Detail(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });

            var post = _repo.GetPost(vm.PostId);

            if (vm.MainCommentId == 0)
            {
                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now

                });
                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                };
                _repo.AddSubComment(comment);

            }
            await _repo.SaveChangesAsync();

            return RedirectToAction("Detail", new { id = vm.PostId });
        }
    }
}
