using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        try
        {
            //var loanApplicationId = await _loanApplicationCommands.UploadFileAsync(request.Name, request.Description);
            return Ok(new FileResponse());
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
            var loanApplicationId = await _loanApplicationCommands.RegisterLoanApplicationAsync(request.Name, request.Description);
            return Ok(new NewLoanApplicationResponse(loanApplicationId));
        }
        catch (Exception e)
        {
            if (e is LoanApplicationPendingException) return Conflict($"User {request.Name} already has a pending application");
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{fileId:Guid}")]
    [ProducesResponseType(typeof(LoanApplicationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoanApplication([FromRoute] Guid fileId)
    {
        try
        {
            var loanApplication = await _loanApplicationQueries.GetLoanApplicationAsync(fileId);
            return Ok(new LoanApplicationResponse(loanApplication.Name, loanApplication.Id));
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
            return Ok(loanApplications.Select(la => new LoanApplicationResponse(la.Name, la.Id)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}