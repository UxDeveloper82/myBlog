using Microsoft.EntityFrameworkCore;
using myBlog.Models;
using myBlog.Models.Comments;
using myBlog.Models.ViewModels;
using myBlog.Repository.IRepository;

namespace myBlog.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public IndexViewModel GetAllPosts(
            int pageNumber, 
            string Category, 
            string search, 
            string orderBy)
        {
            Func<Post, bool> InCategory = (post) => {
                return post.Category.ToLower().Equals(Category.ToLower()); 
            };

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _context.Posts.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => InCategory(x));
            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Title.Contains(search)
                    || x.Body.Contains(search)
                    || x.Description.Contains(search));
            int postsCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);


            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = postsCount > skipAmount + pageSize,
                Category = Category,
                Search = search,
                Posts = query
               .Skip(skipAmount)
               .Take(pageSize)
               .OrderBy(p => p.Created)
               .ToList()
            };

        }

        public Post GetPost(int id)
        {
            return _context.Posts.Include(p => p.MainComments)
                .ThenInclude(m => m.SubComments)
       .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;

        }
    }
}
