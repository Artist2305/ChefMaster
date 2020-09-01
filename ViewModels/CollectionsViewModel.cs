using Dinner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Dinner.ViewModels
{
    public class CollectionsViewModel
    {
        public IPagedList<Recipe> Recipes { get; set; }
        public string Query { get; set; }
    }
}
