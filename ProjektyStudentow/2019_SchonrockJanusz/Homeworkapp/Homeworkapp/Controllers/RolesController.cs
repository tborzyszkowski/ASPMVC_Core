using Homeworkapp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homeworkapp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        public ActionResult Index()
        {
            // Populate Dropdown Lists
            var context = new Models.ApplicationDbContext();

            // Lista ról
            var rolelistDB = context.Roles;
            var rolelistSort = (from r in rolelistDB
                                 orderby r.Name
                                 select r);
            var roles = rolelistSort.ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = roles;

            // Lista użytkowników
            var userlistDB = context.Users;
            var userlistSort = (from u in userlistDB
                                orderby u.UserName
                                select u);
            var users = userlistSort.ToList().Select(u => new SelectListItem { Value = u.UserName.ToString(), Text = u.UserName }).ToList();
            ViewBag.Users = users;

            ViewBag.Message = "";

            return View();
        }


        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {

            try
            {
                var context = new ApplicationDbContext();
                context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.Message = "Pomyślnie utworzono rolę !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string RoleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var context = new ApplicationDbContext();
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                var context = new ApplicationDbContext();
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //  Adding Roles to a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            var context = new ApplicationDbContext();

            if (context == null)
            {
                throw new ArgumentNullException("context", "Context must not be null.");
            }

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.AddToRole(user.Id, RoleName);


            ViewBag.Message = "Pomyślnie utworzono rolę !";

            // Repopulate Dropdown Lists
            // Lista ról
            var rolelistDB = context.Roles;
            var rolelistSort = (from r in rolelistDB
                                orderby r.Name
                                select r);
            var roles = rolelistSort.ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = roles;

            // Lista użytkowników
            var userlistDB = context.Users;
            var userlistSort = (from u in userlistDB
                                orderby u.UserName
                                select u);
            var users = userlistSort.ToList().Select(u => new SelectListItem { Value = u.UserName.ToString(), Text = u.UserName }).ToList();
            ViewBag.Users = users;

            return View("Index");
        }


        //Getting a List of Roles for a User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                var context = new ApplicationDbContext();
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                ViewBag.RolesForThisUser = userManager.GetRoles(user.Id);


                // Repopulate Dropdown Lists
                // Lista ról
                var rolelistDB = context.Roles;
                var rolelistSort = (from r in rolelistDB
                                    orderby r.Name
                                    select r);
                var roles = rolelistSort.ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
                ViewBag.Roles = roles;

                // Lista użytkowników
                var userlistDB = context.Users;
                var userlistSort = (from u in userlistDB
                                    orderby u.UserName
                                    select u);
                var users = userlistSort.ToList().Select(u => new SelectListItem { Value = u.UserName.ToString(), Text = u.UserName }).ToList();
                ViewBag.Users = users;


                ViewBag.Message = "Role pomyślnie odczytano !";
            }

            return View("Index");
        }

        //Deleting a User from A Role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var account = new AccountController();
            var context = new ApplicationDbContext();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);


            if (userManager.IsInRole(user.Id, RoleName))
            {
                userManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.Message = "Pomyślnie usunięto rolę użytkownikowi !";
            }
            else
            {
                ViewBag.Message = "Ten użytkownik nie przynależy do tej roli !";
            }

            // Repopulate Dropdown Lists
            // Lista ról
            var rolelistDB = context.Roles;
            var rolelistSort = (from r in rolelistDB
                                orderby r.Name
                                select r);
            var roles = rolelistSort.ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = roles;

            // Lista użytkowników
            var userlistDB = context.Users;
            var userlistSort = (from u in userlistDB
                                orderby u.UserName
                                select u);
            var users = userlistSort.ToList().Select(u => new SelectListItem { Value = u.UserName.ToString(), Text = u.UserName }).ToList();
            ViewBag.Users = users;

            return View("Index");
        }
    }
}