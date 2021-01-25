using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Hahn.ApplicatonProcess.December2020.Domain.Applicant;
using Hahn.ApplicatonProcess.December2020.Test.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hahn.ApplicatonProcess.December2020.Test.Domain
{
    public class ApplicantDomainTest : ApplicantTestBase
    {
        ApplicantService _applicantService;

        public ApplicantDomainTest()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(this._context);
            ILogger<ApplicantService> logger = new Mock<ILogger<ApplicantService>>().Object;
            _applicantService = new ApplicantService(unitOfWork, logger);
        }

        [Fact]
        public async void CreateApplicantTest()
        {
           var result =  await _applicantService.CreateApplicant(new December2020.Domain.Applicant.Models.CreateApplicantRequest
            {
                Name = "testCreate",
                Address = "testCreateAddress"
            });

            Assert.NotNull(result);
            var findResult = this._context.Applicants.SingleOrDefault(x => x.Name == "testCreate");

            Assert.NotNull(findResult);
        }

        [Fact]
        public async void DeleteApplicantTest()
        {
             await _applicantService.DeleteApplicant(1);

            var findResult = this._context.Applicants.SingleOrDefault(x => x.Id == 1);

            Assert.Null(findResult);
        }

        [Fact]
        public async void UpdateApplicantTest()
        {
            await _applicantService.UpdateApplicant(new December2020.Domain.Applicant.Models.UpdateApplicantRequest
            {
                Id = 1,
                Name = "testCreate",
                Address = "testCreateAddress"
            });

            var findResult = this._context.Applicants.SingleOrDefault(x => x.Id == 1);

            Assert.NotNull(findResult);
            Assert.Equal(1, findResult.Id);
        }
    }
}
