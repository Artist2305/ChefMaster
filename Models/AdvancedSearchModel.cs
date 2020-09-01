using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class AdvancedSearchModel
    {
        public string? Name { get; set; }
        public List<string>? Ingridients { get; set; }
        public List<string>? Tags { get; set; }
        public int? Rating { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorId { get; set; }
        public int? TimeMin { get; set; }
        public int? TimeMax { get; set; }
        public int? DifficultLevel { get; set; }
    }
}
