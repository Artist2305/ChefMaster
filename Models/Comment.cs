using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RecipeId { get; set; }
        public string CommentText { get; set; }
        public DateTime TimeAdded { get; set; }
    }
}
