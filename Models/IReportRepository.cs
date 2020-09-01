using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public interface IReportRepository
    {
        Report Add(Report report);
        Report Delete(int id);
        IEnumerable<Report> GetAll();
        Recipe Get(int id);
        Report Update(Report reportChanges);
    }
}
