using Dinner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList.Mvc.Core;
using X.PagedList;

namespace Dinner.ViewModels
{
    public class AdvancedSearchViewModel : AdvancedSearchModel
    {
        public IPagedList<Recipe> ResultRecipes { get; set; }
    }
}
