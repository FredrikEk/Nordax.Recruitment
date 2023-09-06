using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.Domain.Services;
using Nordax.Bank.Recruitment.Models.LoanApplication;
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
            var fileDetails = file.OpenReadStream();
            byte[] fileContent;

            fileContent = new byte[fileDetails.Length];
            await fileDetails.ReadAsync(fileContent, 0, (int)fileDetails.Length);

            await _loanApplicationService.UploadFileAsync(fileContent);

            return Ok();
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, "Loan Application registered successfully")]
        public async Task<IActionResult> RegisterLoanApplication([Required] [FromBody] RegisterLoanApplicationRequest request)
        {
            await _loanApplicationService.RegisterLoanApplicationAsync(request.FileId);
            return Ok();
        }

        [HttpGet("{fileId:Guid}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loan Application fetched successfully", typeof(LoanApplicationResponse))]
        [ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLoanApplication([FromRoute] Guid fileId)
        {
            await _loanApplicationService.GetLoanApplicationAsync(fileId);

            return Ok();
        }

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, "Loan Application fetched successfully", typeof(LoanApplicationResponse))]
        [ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
        public IActionResult GetLoanApplications()
        {
            _loanApplicationService.GetLoanApplications();
            return Ok();
        }
	}
}