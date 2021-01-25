using System;
using System.Data;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Data.Repositories;

namespace Hahn.ApplicatonProcess.December2020.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            ApplicantRepository = new ApplicantRepository(_context);
        }

        public IRepository<Applicant> ApplicantRepository { get; }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
