using Microsoft.AspNetCore.Mvc.Rendering;

namespace myBlog.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Author = "Author";
        public static string Member = "Member";

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin)
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value=Helper.Admin,Text=Helper.Admin}
                };
            }
            else
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Value=Helper.Member,Text=Helper.Member},
                    new SelectListItem{Value=Helper.Author,Text=Helper.Author}
                };
            }
        }





    }

}
