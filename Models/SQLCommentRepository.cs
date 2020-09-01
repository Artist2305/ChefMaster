using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class SQLCommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public SQLCommentRepository(AppDbContext context)
        {
            this._context = context;
        }
        public Comment GetComment(int recipeId)
        {
            return _context.Comments.Where(x => x.RecipeId == recipeId).FirstOrDefault();
        }
        public Comment Add(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }
        public Comment Update(Comment changedComment)
        {
            var comment = _context.Comments.Attach(changedComment);
            comment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return changedComment;
        }
        public Comment Delete(int id)
        {
            Comment comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
            return comment;
        }
        public IEnumerable<Comment> GetAllCommentsOfRecipe(int recipeId)
        {
            return _context.Comments.Where(e => e.RecipeId == recipeId);
        }
    }
}
