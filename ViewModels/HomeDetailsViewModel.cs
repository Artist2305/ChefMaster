using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dinner.Models;


namespace Dinner.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Recipe Recipe { get; set; }
        public string PageTitle { get; set; }

        [Display(Name = "Ingridient")]
        public List<string> Ingridients { get; set; }
        [Display(Name = "Amount")]
        public List<string> IngridientsAmountNumber { get; set; }
        [Display(Name = "Portion")]
        public List<string> IngridientsAmountKind { get; set; }
        [Display(Name = "Tag")]
        public List<string> Tags { get; set; }
        public int Time { get; set; }
        public List<Recipe> ThreeRandomOtherRecipe { get; set; }
        public List<Recipe> ThreeRandomUserRecipe { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        [Display(Name="TYPE COMMENT")]
        public string NewComment { get; set; }
        [Display(Name = "TYPE REPORT")]
        public string NewReport { get; set; }
    }
}
