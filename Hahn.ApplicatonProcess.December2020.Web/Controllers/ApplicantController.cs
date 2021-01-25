using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Domain.Applicant;
using Hahn.ApplicatonProcess.December2020.Web.Models.Applicant;
using Microsoft.AspNetCore.Http;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly ILogger<ApplicantController> _logger;
        private readonly IApplicantService _applicantService;
        public ApplicantController(ILogger<ApplicantController> logger, IApplicantService applicantService)
        {
            _logger = logger;
            _applicantService = applicantService;
        }


        /// <summary>
        /// Creates a applicant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Applicant
        ///     {
        ///        "name": "Avinash",
        ///        "familyName": "Kumar",
        ///        "address": "Dubai, UAE",
        ///        "countryOfOrigin": "UAE",
        ///        "emailAddress": "tharwania@gmail.com",
        ///        "age": 26,
        ///        "hired": true
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created applicant</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is invalid</response>  
        [HttpPost()]
        public async Task<IActionResult> CreateAsync(CreateApplicantRequest request)
        {
            try
            {
                var response = await _applicantService.CreateApplicant(new Domain.Applicant.Models.CreateApplicantRequest()
                {
                    Name = request.Name,
                    FamilyName = request.FamilyName,
                    Address = request.Address,
                    CountryOfOrigin = request.CountryOfOrigin,
                    EmailAddress = request.EmailAddress,
                    Age = request.Age,
                    Hired = request.Hired,
                });

                return CreatedAtRoute("Get", new { id = response.Id }, response);
            }
            catch (System.Exception ex)
            {
                this._logger.LogError(ex, "error while create");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets applicant by Id.
        /// </summary>
        /// <param name="id"></param>  
        [HttpGet("id", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var response = await _applicantService.GetApplicant(id);
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(new GetApplicantResponse()
                {
                    Id = response.Id,
                    Name = response.Name,
                    FamilyName = response.FamilyName,
                    Address = response.Address,
                    CountryOfOrigin = response.CountryOfOrigin,
                    EmailAddress = response.EmailAddress,
                    Age = response.Age,
                    Hired = response.Hired,
                });

            }
            catch (System.Exception ex)
            {
                this._logger.LogError(ex, "error while get");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates applicant given applicant is available.
        /// </summary>
        /// <param name="request"></param> 
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(UpdateApplicantRequest request)
        {
            try
            {
                await _applicantService.UpdateApplicant(new Domain.Applicant.Models.UpdateApplicantRequest()
                {
                    Id = request.Id,
                    Name = request.Name,
                    FamilyName = request.FamilyName,
                    Address = request.Address,
                    CountryOfOrigin = request.CountryOfOrigin,
                    EmailAddress = request.EmailAddress,
                    Age = request.Age,
                    Hired = request.Hired,
                });

                return Ok();
            }
            catch (System.Exception ex)
            {
                this._logger.LogError(ex, "error while update");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes the applicant by Id.
        /// </summary>
        /// <param name="id"></param> 
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _applicantService.DeleteApplicant(id);

                return Ok();
            }
            catch (System.Exception ex)
            {
                this._logger.LogError(ex, "error while delete");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
