using Dinner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Recipe> LastRecipes { get; set; }
        public IEnumerable<Recipe> LastBestRecipes { get; set; }
    }
}
