using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.December2020.Test.BaseClasses
{
    public class ApplicantTestBase:  IDisposable
    {
        protected ApplicationContext _context;
        public ApplicantTestBase()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            _context = new ApplicationContext(options);

            _context.Database.EnsureCreated();
            Seed(_context);

        }

        private void Seed(ApplicationContext context)
        {
            context.Applicants.AddRange(new List<Applicant>()
            {
                new Applicant
                {
                    Name = "Avi",
                    FamilyName = "Me",
                    Address = "Dubai"
                },
                new Applicant
                {
                    Name = "Avi2",
                    FamilyName = "Me",
                    Address = "Dubai"
                },new Applicant
                {
                    Name = "Avi3",
                    FamilyName = "Me",
                    Address = "Dubai"
                },
                new Applicant
                {
                    Name = "Avi4",
                    FamilyName = "Me",
                    Address = "Dubai"
                }
            });
            context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Database.EnsureDeleted();
            _context?.Dispose();
        }
    }
}
