using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public interface ICommentRepository
    {
        Comment GetComment(int id);
        Comment Add(Comment comment);
        Comment Update(Comment comment);
        Comment Delete(int id);
        IEnumerable<Comment> GetAllCommentsOfRecipe(int recipeId);
    }
}
