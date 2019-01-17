using Homeworkapp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Homeworkapp.Startup))]
namespace Homeworkapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoleAndUsers();
        }

        // Utwórz domyślne role i konto administratora
        private void CreateRoleAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Utwórz rolę admina a następnie utwórz konto Administratora
            if (!roleManager.RoleExists("Admin"))
            {
                // Utwórz rolę
                var role = new IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                // Utwórz konto Administratora
                var user = new ApplicationUser();
                user.UserName = "administrator@mail.edu.pl";
                user.Email = "administrator@mail.edu.pl";
                string userPassword = "Test1!";

                var chkUser = UserManager.Create(user, userPassword);

                // Nadaj domyślnie rolę Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Administrator");
                }

            }

            // Utwórz rolę Moderatora
            if (!roleManager.RoleExists("Moderator"))
            {
                var role = new IdentityRole();
                role.Name = "Moderator";
                roleManager.Create(role);
            }
        }
    }
}
