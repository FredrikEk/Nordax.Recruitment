using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.Domain.Services;
using Nordax.Bank.Recruitment.Models.LoanApplication;
using Nordax.Bank.Recruitment.Models.Subscriber;
using Swashbuckle.AspNetCore.Annotations;
using static System.Net.WebRequestMethods;

namespace Nordax.Bank.Recruitment.Controllers
{
    [ApiController]
    [Route("api/loan-application")]
    public class LoanApplicationController : ControllerBase
    {
        private readonly ILoanApplicationService _loanApplicationService;
        public LoanApplicationController(ILoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }

        [HttpPost("attachment")]
        [SwaggerResponse(StatusCodes.Status200OK, "File uploaded successfully", typeof(FileResponse))]
        [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var fileDetails = file.OpenReadStream();
                byte[] fileContent;

                fileContent = new byte[fileDetails.Length];
                await fileDetails.ReadAsync(fileContent, 0, (int)fileDetails.Length);

                await _loanApplicationService.UploadFileAsync(fileContent);

                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Loan Application registered successfully")]
        public async Task<IActionResult> RegisterLoanApplication([Required] [FromBody] RegisterLoanApplicationRequest request)
        {
            try
            {
                var loanApplicationId = await _loanApplicationService.RegisterLoanApplicationAsync(request.FileId);
                return Ok(new LoanApplicationResponse(loanApplicationId));
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{fileId:Guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loan Application fetched successfully", typeof(LoanApplicationResponse))]
        [ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLoanApplication([FromRoute] Guid fileId)
        {
            try
            {
                var result = await _loanApplicationService.GetLoanApplicationAsync(fileId);
                return Ok(result);
            }
            catch (Exception e)
            {
                if (e is LoanApplicationNotFoundException) return NotFound("No loan application found with that id");
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loan Application fetched successfully", typeof(LoanApplicationResponse))]
        [ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
        public IActionResult GetLoanApplications()
        {
            try
            {
                var result = _loanApplicationService.GetLoanApplications();
                return Ok(result);
            }
            catch(Exception e)
            {
                if (e is LoanApplicationNotFoundException) return NotFound("No loan applications found");
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
	}
}