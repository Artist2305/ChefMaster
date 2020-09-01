using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [NotMapped]
        public string EncryptedId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Hint { get; set; }
        public int Rating { get; set; }
        public string Ingridients { get; set; }
        public string Tags { get; set; }
        public string PhotoPatch { get; set; }
        public string AuthorId { get; set; }
        public int AmountOfRates { get; set; }
        public int Time { get; set; }
        public DateTime AddedTime { get; set; }
        public int DifficultLevel { get; set; }
    }
}
