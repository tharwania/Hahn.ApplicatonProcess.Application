using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Domain.Applicant.Models;

namespace Hahn.ApplicatonProcess.December2020.Domain.Applicant
{
    public interface IApplicantService
    {
        Task<CreateApplicantResponse> CreateApplicant(CreateApplicantRequest request);
        Task<GetApplicantResponse> GetApplicant(int id);
        Task UpdateApplicant(UpdateApplicantRequest updateApplicantRequest);
        Task DeleteApplicant(int id);
    }
}
