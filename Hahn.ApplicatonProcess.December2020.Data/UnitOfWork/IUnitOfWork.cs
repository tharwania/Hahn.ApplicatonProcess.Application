using System;
using System.Data;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Data.Repositories;

namespace Hahn.ApplicatonProcess.December2020.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Applicant> ApplicantRepository { get; }
        Task<int> SaveChanges();
    }
}
