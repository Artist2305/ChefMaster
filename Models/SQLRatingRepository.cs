using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class SQLRatingRepository : IRateRepository
    {
        private readonly AppDbContext _context;
        public SQLRatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public Rate Add(Rate rate)
        {
            if (_context.Rates.Where(x => x.UserId == rate.UserId && x.RecipeId == rate.RecipeId).FirstOrDefault() == null)
            {
                _context.Rates.Add(rate);
                _context.SaveChanges();
            }
            return rate;
        }
        public Rate Get(string userId, int recipeId)
        {
            return _context.Rates.Where(x => x.UserId == userId && x.RecipeId == recipeId).FirstOrDefault();
        }

        public bool IsRatedByUser(string userId, int recipeId)
        {
            return  _context.Rates.Where(x => x.UserId == userId && x.RecipeId == recipeId).FirstOrDefault() != null ? true : false;
        }
        public short GetOveralRating(int recipeId)
        {
            List<Rate> recipes = new List<Rate>();
            recipes = _context.Rates.Where(x => x.RecipeId == recipeId).ToList();

            short val = 0;

            foreach (var rate in recipes)
            {
                val += rate.RateValue;
            }

            val /= (short)recipes.Count;

            return val;
        }
        public IEnumerable<Rate> GetAllWithId(int recipeId)
        {
            return _context.Rates.Where(x => x.RecipeId == recipeId);

        }

    }
}
