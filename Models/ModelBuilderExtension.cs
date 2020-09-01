using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasData(
               new Recipe
               {
                   Id = 1,
                   Name = "Coocked chicken breast",
                   Description = "Awesome taste! Awesome taste! Awesome taste! Awesome taste! Awesome taste! "
               },
               new Recipe
               {
                   Id = 2,
                   Name = "Cookies",
                   Description = "Awesome taste! Awesome taste! Awesome taste! Awesome taste! Awesome taste! "
               }
           );
        }
    }
}
