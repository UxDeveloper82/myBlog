using myBlog.Models;
using myBlog.Models.Comments;
using myBlog.Models.ViewModels;

namespace myBlog.Repository.IRepository
{
    public interface IRepository
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();

        IndexViewModel GetAllPosts(int pageNumber, string Category, string search, string orderBy);

        void AddPost(Post post);

        void UpdatePost(Post post);

        void RemovePost(int id);

        void AddSubComment(SubComment comment);

        Task<bool> SaveChangesAsync();

    }
}
