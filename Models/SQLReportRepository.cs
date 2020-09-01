using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.Models
{
    public class SQLReportRepository : IReportRepository
    {
        private readonly AppDbContext context;
        public SQLReportRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Report Add(Report report)
        {
            context.Reports.Add(report);
            context.SaveChanges();
            return report;
        }

        public Report Delete(int id)
        {
            Report report = context.Reports.Find(id);
            if (report != null)
            {
                context.Reports.Remove(report);
                context.SaveChanges();
            }
            return report;
        }

        public IEnumerable<Report> GetAll()
        {
            return context.Reports;
        }

        public Recipe Get(int id)
        {
            return context.Recipes.Find(id);
        }

        public Report Update(Report reportChanges)
        {
            var report = context.Reports.Attach(reportChanges);
            report.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return reportChanges;
        }
    }
}
