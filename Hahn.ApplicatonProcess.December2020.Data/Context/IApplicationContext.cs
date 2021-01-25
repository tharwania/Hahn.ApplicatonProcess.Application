using System.Threading;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Data.Context
{
    public interface IApplicationContext
    {
        public DbSet<Applicant> Applicants { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
