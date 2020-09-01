using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class SQLFavouriteRepository : IFavouriteRepository
    {
        private readonly AppDbContext context;
        public SQLFavouriteRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Favourite Add(Favourite favourite)
        {
            if (context.Favourites.Where(x => x.UserId == favourite.UserId && x.RecipeObserverdId == favourite.RecipeObserverdId).FirstOrDefault() == null)
            {
                context.Favourites.Add(favourite);
                context.SaveChanges();
            }
            return favourite;
        }
        public IEnumerable<Favourite> GetAllWithId(string userId) 
        {
            return context.Favourites.Where(e => e.UserId == userId);
        }
        public IEnumerable<Favourite> GetAllWithRecipeId(int recipeId)
        {
            return context.Favourites.Where(e => e.RecipeObserverdId == recipeId);
        }
        public Favourite Remove(int favouriteId)
        {
            Favourite favourite = context.Favourites.Find(favouriteId);
            if (favourite != null)
            {
                context.Favourites.Remove(favourite);
                context.SaveChanges();
            }
            return favourite;
        }
        public bool IsFavouriteOfUser(string userId, int recipeObserverId)
        {
            return context.Favourites.Where(x => x.UserId == userId && x.RecipeObserverdId == recipeObserverId).FirstOrDefault() != null ? true : false;
        }
    }
}
