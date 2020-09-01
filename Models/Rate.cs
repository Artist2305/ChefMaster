using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class Rate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public int RecipeId{ get; set; }
        [Required]
        public short RateValue { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [NotMapped]
        public int TempOveralRating { get; set; }
        [NotMapped]
        public int TempAmountOfRates { get; set; }
    }
}
