using FoodApp.Api.FoodApp.Core.Entities;
using FoodApp.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Api.Data
{
    public class Context : IdentityDbContext<ApplicationUser>

    {
        public Context(DbContextOptions<Context> options) : base(options) { }


        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }

        public DbSet<Item> items { get; set; }  
        public DbSet<RoleFeature> RoleFeature { get; set; }


    }
}
