using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class SQLRecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext context;
        public SQLRecipeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Recipe Add(Recipe recipe)
        {
            context.Recipes.Add(recipe);
            context.SaveChanges();
            return recipe;
        }

        public Recipe Delete(int id)
        {
            Recipe recipe = context.Recipes.Find(id);
            if (recipe != null)
            {
                context.Recipes.Remove(recipe);
                context.SaveChanges();
            }
            return recipe;
        }

        public IEnumerable<Recipe> GetAllRecipe()
        {
            return context.Recipes;
        }
        public IEnumerable<Recipe> GetNewRecipes()
        {
            var recipeList = context.Recipes.ToList();
            recipeList.Sort((x, y) => DateTime.Compare(x.AddedTime, y.AddedTime));
            var elements = recipeList.Take(15);
            return elements;
        }
        public IEnumerable<Recipe> GetBestRecipesFromLastDays()
        {
            DateTime today = DateTime.Today;
            DateTime lastDay = today.AddDays(-30);

            var recipeList = context.Recipes.ToList();
            var lastRecipes = recipeList.Where(e => e.AddedTime <= today && e.AddedTime >= lastDay).ToList();
            var values = lastRecipes.OrderByDescending(p => p.Rating).ToList();
            var elements = values.Take(15);
            return elements;
        }

        public Recipe GetRecipe(int id)
        {
            return context.Recipes.Find(id);
        }

        public Recipe Update(Recipe recipeChanges)
        {
            var recipe = context.Recipes.Attach(recipeChanges);
            recipe.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return recipeChanges;
        }
        public IEnumerable<Recipe> GetAllBreakfastRecipes()
        {
            return context.Recipes;
        }
        public IEnumerable<Recipe> GetAllRecipesWithQuery(string querry)
        {
            if (querry != null) {
                return context.Recipes.Where(e => e.Name.ToLower().Contains(querry));
            }
            else {
                return context.Recipes;
            }
        }
        public IEnumerable<Recipe> GetAllRecipesOfUser(string userId)
        {
            return context.Recipes.Where(e => e.AuthorId == userId);
        }
        public IEnumerable<Recipe> GetThreeRandomUserRecipes(string userId)
        {
            List<Recipe> _recipes = context.Recipes.Where(e => e.AuthorId == userId).ToList();
            Random rand = new Random();
            List<Recipe> chosedRecipes = new List<Recipe>();
            int amount = _recipes.Count();
            if (amount > 3)
            {
                amount = 3;
            }
            for (int i =0; i < amount; i++)
            {
                int index = rand.Next(0, _recipes.Count());
                chosedRecipes.Add(_recipes.ElementAt(index));
                _recipes.RemoveAt(index);
            }
            return chosedRecipes;
        }
        public IEnumerable<Recipe> GetThreeRandomRecipes()
        {
            List<Recipe> _recipes = context.Recipes.ToList();
            Random rand = new Random();
            List<Recipe> chosedRecipes = new List<Recipe>();

            for (int i = 0; i < 3; i++)
            {
                int index = rand.Next(0, _recipes.Count());
                chosedRecipes.Add(_recipes.ElementAt(index));
            }

            return chosedRecipes;
        }
        public Recipe GetRandomRecipe()
        {
            Random rand = new Random();
            int index = rand.Next(0, context.Recipes.Count());
            List<Recipe> _recipes = context.Recipes.ToList();
            Recipe recipe = _recipes.ElementAt(index);

            return recipe;
        }
        public IEnumerable<Recipe> GetAllRecipesForCollections(string querry)
        {
            return context.Recipes.Where(e => e.Tags.ToLower().Contains(querry));
        }
        public IQueryable<Recipe> GetAdvancedSearchRecipes(AdvancedSearchModel searchModel)
        {
            var result = context.Recipes.AsQueryable();
            if (searchModel != null)
            {    
                if (!string.IsNullOrEmpty(searchModel.Name))
                {
                    result = result.Where(e => e.Name.ToLower().Contains(searchModel.Name));
                }
                
                if (searchModel.Tags != null)
                {
                    foreach (var tag in searchModel.Tags)
                    {
                        if (tag != null)
                            result = result.Where(x => x.Tags.ToLower().Contains(tag));
                    }          
                }
                if (searchModel.Ingridients != null)
                {
                    foreach (var ingridient in searchModel.Ingridients)
                    {
                        if(ingridient != null)
                        result = result.Where(x => x.Ingridients.ToLower().Contains(ingridient));
                    }
                }
                if (searchModel.DifficultLevel.HasValue)
                {
                    result = result.Where(x => x.DifficultLevel == searchModel.DifficultLevel);
                }
                if (searchModel.TimeMin.HasValue || (searchModel.TimeMax.HasValue))
                {
                    if (!searchModel.TimeMin.HasValue)
                        searchModel.TimeMin = 0;
                    if (!searchModel.TimeMax.HasValue)
                        searchModel.TimeMax = 999999999;
                    result = result.Where(x => x.Time >= searchModel.TimeMin && x.Time <= searchModel.TimeMax);
                }
                if (searchModel.Rating.HasValue)
                {
                    result = result.Where(x => x.Rating == searchModel.Rating);
                }
            } 
            return result;
        }
    }
}
