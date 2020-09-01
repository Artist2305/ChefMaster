using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public interface IRecipeRepository
    {
        Recipe GetRecipe(int id);
        IEnumerable<Recipe> GetAllRecipe();
        IEnumerable<Recipe> GetNewRecipes();
        IEnumerable<Recipe> GetBestRecipesFromLastDays();
        Recipe Add(Recipe recipe);
        Recipe Update(Recipe recipeChanges);
        Recipe Delete(int id);
        IEnumerable<Recipe> GetAllBreakfastRecipes();
        IEnumerable<Recipe> GetAllRecipesWithQuery(string querry);
        IEnumerable<Recipe> GetAllRecipesOfUser(string userId);
        IEnumerable<Recipe> GetThreeRandomUserRecipes(string userId);
        IEnumerable<Recipe> GetThreeRandomRecipes();
        Recipe GetRandomRecipe();
        IEnumerable<Recipe> GetAllRecipesForCollections(string querry);
        IQueryable<Recipe> GetAdvancedSearchRecipes(AdvancedSearchModel searchModel);
    }
}
