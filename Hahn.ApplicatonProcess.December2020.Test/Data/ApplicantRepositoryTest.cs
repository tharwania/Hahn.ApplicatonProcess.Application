using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Hahn.ApplicatonProcess.December2020.Test.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Hahn.ApplicatonProcess.December2020.Test.Data
{
    public class ApplicantRepositoryTest : ApplicantTestBase
    {
      
        [Fact]
        public async void FindApplicantTest()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(_context);

            var result = await unitOfWork.ApplicantRepository.Find(x => x.Name == "Avi");
            Assert.NotNull(result);
            Assert.Equal("Avi", result.Name);
        }

        [Fact]
        public async void AddApplicantTest()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(_context);

            var result = await unitOfWork.ApplicantRepository.InsertAsync(new December2020.Data.Entities.Applicant
            {
                Name = "TestInsert"
            });
            await unitOfWork.SaveChanges();
            Assert.NotNull(result);
            Assert.Equal("TestInsert", result.Name);
        }

        [Fact]
        public async void DeleteApplicantTest()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(_context);

            var result = await unitOfWork.ApplicantRepository.Find(x => x.Name == "Avi");
             unitOfWork.ApplicantRepository.Delete(result);
             
            await unitOfWork.SaveChanges();

            var resultAfterDeleted = await unitOfWork.ApplicantRepository.Find(x => x.Name == "Avi");
            Assert.Null(resultAfterDeleted);
        }

    }
}
