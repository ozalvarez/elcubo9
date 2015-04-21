namespace elcubo9.data.Migrations
{
    using elcubo9.data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<elcubo9.data.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(elcubo9.data.Models.ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser userRoot = null;
            if (userManager.FindByName("oz@elcubo9.com") == null)
            {
                userRoot = new ApplicationUser { UserName = "oz@elcubo9.com", Email = "oz@elcubo9.com" };
                userManager.Create(userRoot, "123456");

                roleManager.Create(new IdentityRole { Name = "admin" });

                userManager.AddToRole(userRoot.Id, "admin");
            }
            else
            {
                userRoot = userManager.FindByName("oz@elcubo9.com");
            }
        }
    }
}
