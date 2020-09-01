using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public interface IObserveRepository
    {
        Observe Add(Observe observe);
        public IEnumerable<Observe> GetObserversByUserId(string userId);
        public IEnumerable<Observe> GetObservedChefsByUserId(string userId);
        Observe Remove(string observeUserId);
        bool IsObservedOfUser(string userId, string observeUserId);
    }
}
