using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Collections.Generic;
using System;
namespace elcubo9.data.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual List<CustomerUser> CustomerUsers { get; set; }
        public virtual List<Order> Orders { get; set; }
        public bool Block { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public ApplicationUser() { }
        public ApplicationUser(string Email, string Name)
        {
            this.Email = Email;
            this.UserName = Email;
            this.Name = Name;
            this.DateCreated = DateTime.Now;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerUser> CustomerUsers { get; set; }
        public DbSet<CustomerUserRol> CustomerUserRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuTag> MenuTags { get; set; }
        public DbSet<MenuAdditional> MenuAdditionals { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<AdditionalItem> AdditionalItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenu> OrderMenus { get; set; }
        public DbSet<OrderMenuAdditional> OrderMenuAdditionals { get; set; }
        public DbSet<CustomerEmail> CustomerEmails { get; set; }
        public DbSet<OrderEmailSent> OrderEmailsSent { get; set; }
    }
}