using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.Domain.Interfaces.Commands;
using Nordax.Bank.Recruitment.Domain.Interfaces.Queries;
using Nordax.Bank.Recruitment.Models.LoanApplication;

namespace Nordax.Bank.Recruitment.Controllers;

[ApiController]
[Route("api/loan-application")]
public class LoanApplicationController : ControllerBase
{
    private readonly ILoanApplicationCommands _loanApplicationCommands;
    private readonly ILoanApplicationQueries _loanApplicationQueries;
    public LoanApplicationController(ILoanApplicationCommands loanApplicationCommands, ILoanApplicationQueries loanApplicationQueries)
    {
        _loanApplicationCommands = loanApplicationCommands;
        _loanApplicationQueries = loanApplicationQueries;
    }

    [HttpPost("attachment")]
    [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        //Hade jag lagt mer tid så hade jag gjort en generisk filhanterare som accepterar enbart
        //specifika filer enligt kravspec och eventuellt begränsa storlek
        try
        {
            byte[] content;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                content = stream.ToArray();
            }

            var fileId = await _loanApplicationCommands.UploadFileAsync(file.FileName, file.ContentType, content);
            return Ok(new FileResponse(fileId));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterLoanApplication([Required] [FromBody] NewLoanApplicationRequest request)
    {
        try
        {
            var loanApplicationId = await _loanApplicationCommands.RegisterLoanApplicationAsync(request.Name, request.FileId, request.Description);
            return Ok(new NewLoanApplicationResponse(loanApplicationId));
        }
        catch (Exception e)
        {
            if (e is LoanApplicationPendingException) return Conflict($"User {request.Name} already has a pending application");
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{loanApplicationId:Guid}")]
    [ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoanApplication([FromRoute] Guid loanApplicationId)
    {
        try
        {
            var loanApplication = await _loanApplicationQueries.GetLoanApplicationAsync(loanApplicationId);
            return Ok(new LoanApplicationResponse(loanApplication.Id, loanApplication.Name, loanApplication.FileId));
        }
        catch (Exception e)
        {
            if (e is LoanApplicationNotFoundException) return NotFound("No application found with that id");
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<LoanApplicationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoanApplications()
    {
        try
        {
            var loanApplications = await _loanApplicationQueries.GetLoanApplicationsAsync();
            return Ok(loanApplications.Select(la => new LoanApplicationResponse(la.Id, la.Name, la.FileId)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}