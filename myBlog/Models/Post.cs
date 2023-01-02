using myBlog.Models.Comments;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace myBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = "";
        [Required]
        public string Body { get; set; } = "";
        public string Image { get; set; } = "";
        [Required]
        [DisplayName("Enter Description")]
        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;

        public List<MainComment> MainComments { get; set; }
    }
}
