using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class SQLObserveRepository : IObserveRepository
    {
        private readonly AppDbContext context;
        public SQLObserveRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Observe Add(Observe observe)
        {
            if (context.Observed.Where(x => x.UserId == observe.UserId && x.UserObserverdId == observe.UserObserverdId).FirstOrDefault() == null)
            {
                context.Observed.Add(observe);
                context.SaveChanges();
            }
            return observe;
        }
        public IEnumerable<Observe> GetObserversByUserId(string userId)
        {
            return context.Observed.Where(e => e.UserObserverdId == userId);
        }
        public IEnumerable<Observe> GetObservedChefsByUserId(string userId)
        {
            return context.Observed.Where(e => e.UserId == userId);
        }
        public Observe Remove(string observeUserId)
        {
            Observe observe = context.Observed.Find(observeUserId);
            if (observe != null)
            {
                context.Observed.Remove(observe);
                context.SaveChanges();
            }
            return observe;
        }
        public bool IsObservedOfUser(string userId, string observeUserId)
        {
            return context.Observed.Where(x => x.UserId == userId && x.UserObserverdId == observeUserId).FirstOrDefault() != null ? true : false;
        }
    }
}
