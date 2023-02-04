using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Emit;
using myBlog.Data;
using myBlog.Enums;
using myBlog.Models;

namespace myBlog.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<BlogUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        private async Task SeedRolesAsync()
        {
            //If there are already Roles in the system, do nothing. 
            if (_context.Roles.Any())
            {
                //communication with the Roles table.
                return;
            }

            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                //need to use the role Manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            
            }
        }

        private async Task SeedUserAsync()
        {
            //If there are already Users in the system, do nothing.
            if (_context.Users.Any()) //checking users table for users
            {
                return;
            }

            //step 1: Creates a new Instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "electronicsmf@aol.com",
                UserName = "uxdeveloper82@aol.com",
                FirstName = "Martin",
                LastName = "Fernandez",
                DisplayName = "Uxdeveloper82",
                EmailConfirmed = true
            };
            //step 2: Use the UserManager top create a new user that is defined by adminUser

            await _userManager.CreateAsync(adminUser, "Peru1982#");

            //step 3: Add this new user to Administrator role

            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());
            //BlogRole is the Enum to assign a role 

            //step 1 repeat: Create the moredator user
            var modUser = new BlogUser()
            {
                Email = "electronicsmf@gmail.com",
                UserName = "electronicsmf@gmail.com",
                FirstName = "Martin",
                LastName = "Fernandez",
                DisplayName = "electronicsmf",
                EmailConfirmed = true
            };
            await _userManager.CreateAsync(modUser, "Peru1982#");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}
