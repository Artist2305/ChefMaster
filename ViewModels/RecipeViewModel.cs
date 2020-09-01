using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.ViewModels
{
    public class RecipeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Hint { get; set; }
        public int Rating { get; set; }
        public IFormFile Photo { get; set; }
        [Display(Name = "Time required")]
        [Required]
        public int Time { get; set; }
        public string AuthorId { get; set; }
        [Display(Name = "Ingridient")]
        public List<string> Ingridients { get; set; }
        [Display(Name = "Tag")]
        public List<string> Tags { get; set; }
        public DateTime AddedTime { get; set; }
        [Display(Name = "Difficult")]
        public int DifficultLevel { get; set; }

    }
}
