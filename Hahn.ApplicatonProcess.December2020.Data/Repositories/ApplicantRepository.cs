using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories
{
    public class ApplicantRepository : IRepository<Applicant>
    {
        private readonly ApplicationContext _context;

        public ApplicantRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public async Task<Applicant> Find(Expression<Func<Applicant, bool>> predicate)
        {
            return await this._context.Applicants.SingleOrDefaultAsync(predicate);
        }
        public async Task<Applicant> InsertAsync(Applicant entity)
        {
            await this._context.Applicants.AddAsync(entity);

            return entity;
        }

        public async void InsertRange(IEnumerable<Applicant> entities)
        {
            await this._context.Applicants.AddRangeAsync(entities);
        }

        public void Update(Applicant entity)
        {
            this._context.Applicants.Update(entity);
        }

        public void Delete(Applicant entity)
        {
            this._context.Applicants.Remove(entity);
        }

        public IQueryable<Applicant> Queryable()
        {
            return this._context.Applicants.AsQueryable();
        }
    }
}
