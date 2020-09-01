using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public interface IRateRepository
    {
        Rate Add(Rate rate);
        Rate Get(string userId, int recipeId);
        IEnumerable<Rate> GetAllWithId(int recipeId);
        bool IsRatedByUser(string userId, int recipeId);
        short GetOveralRating(int recipeId);
    }
}
