using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public interface IFavouriteRepository
    {
        Favourite Add(Favourite favourite);
        IEnumerable<Favourite> GetAllWithId(string userId);
        Favourite Remove(int recipeId);
        bool IsFavouriteOfUser(string userId, int recipeId);
        IEnumerable<Favourite> GetAllWithRecipeId(int recipeId);
    }
}
