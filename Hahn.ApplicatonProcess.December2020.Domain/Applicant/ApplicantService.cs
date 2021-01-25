using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Hahn.ApplicatonProcess.December2020.Domain.Applicant.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.December2020.Domain.Applicant
{
    public class ApplicantService : IApplicantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApplicantService> _logger;
        public ApplicantService(IUnitOfWork unitOfWork, ILogger<ApplicantService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CreateApplicantResponse> CreateApplicant(CreateApplicantRequest request)
        {
            try
            {
                var response = await _unitOfWork.ApplicantRepository.InsertAsync(new Data.Entities.Applicant()
                {
                    Name = request.Name,
                    FamilyName = request.FamilyName,
                    Address = request.Address,
                    CountryOfOrigin = request.CountryOfOrigin,
                    EmailAddress = request.EmailAddress,
                    Age = request.Age,
                    Hired = request.Hired,
                });

                await _unitOfWork.SaveChanges();

                return new CreateApplicantResponse()
                {
                    Id = response.Id,
                    Name = response.Name,
                    FamilyName = response.FamilyName,
                    Address = response.Address,
                    CountryOfOrigin = response.CountryOfOrigin,
                    EmailAddress = response.EmailAddress,
                    Age = response.Age,
                    Hired = response.Hired,
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError("CreateApplicant has an error", ex);
                throw;
            }
        }

        public async Task<GetApplicantResponse> GetApplicant(int id)
        {
            try
            {
                var response = await _unitOfWork.ApplicantRepository.Find(applicant => applicant.Id == id);

                if (response == null)
                {
                    return null;
                }

                return new GetApplicantResponse()
                {
                    Id = response.Id,
                    Name = response.Name,
                    FamilyName = response.FamilyName,
                    Address = response.Address,
                    CountryOfOrigin = response.CountryOfOrigin,
                    EmailAddress = response.EmailAddress,
                    Age = response.Age,
                    Hired = response.Hired,
                };
            }
            catch (Exception ex)
            {
                this._logger.LogError("GetApplicant has an error {id}", ex, id);
                throw;
            }
        }

        public async Task UpdateApplicant(UpdateApplicantRequest updateApplicantRequest)
        {
            try
            {
                var entity = await this._unitOfWork
                       .ApplicantRepository
                       .Find(applicant => applicant.Id == updateApplicantRequest.Id);

                if (entity == null)
                {
                    throw new ObjectNotFoundException();
                }

                entity.Address = updateApplicantRequest.Address;
                entity.Name = updateApplicantRequest.Name;
                entity.FamilyName = updateApplicantRequest.FamilyName;
                entity.CountryOfOrigin = updateApplicantRequest.CountryOfOrigin;
                entity.EmailAddress = updateApplicantRequest.EmailAddress;
                entity.Age = updateApplicantRequest.Age;
                entity.Hired = updateApplicantRequest.Hired;

                this._unitOfWork.ApplicantRepository.Update(entity);
                await this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                this._logger.LogError("UpdateApplicant has an error with {id}", ex, updateApplicantRequest.Id);
                throw;
            }
        }

        public async Task DeleteApplicant(int id)
        {
            try
            {
                var entity = await this._unitOfWork
                        .ApplicantRepository
                        .Find(applicant => applicant.Id == id);

                if (entity == null)
                {
                    throw new Exception();
                }

                this._unitOfWork.ApplicantRepository.Delete(entity);
                await this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                this._logger.LogError("DeleteApplicant has an error with {id}", ex, id);
                throw;
            }
        }
    }
}
