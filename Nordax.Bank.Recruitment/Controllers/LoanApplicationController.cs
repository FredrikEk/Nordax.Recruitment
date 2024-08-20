using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nordax.Bank.Recruitment.Domain.Interfaces.Commands;
using Nordax.Bank.Recruitment.Domain.Interfaces.Queries;
using Nordax.Bank.Recruitment.Models.LoanApplication;

namespace Nordax.Bank.Recruitment.Controllers;

[ApiController]
[Route("api/loan-application")]
public class LoanApplicationController : ControllerBase
{
    private readonly ILoanCommands _loanapplicationCommands;
    private readonly ILoanQueries _loanapplicationQueries;
    public LoanApplicationController(ILoanCommands loanapplicationCommands, ILoanQueries loanapplicationQueries)
    {
        _loanapplicationCommands = loanapplicationCommands;
        _loanapplicationQueries = loanapplicationQueries;
    }

    [HttpPost("attachment")]
    [ProducesResponseType(typeof(FileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        return BadRequest("No file uploaded.");
        try {
            var uploadFileId =  await _loanapplicationCommands.UploadFileAsync(file);
            return Ok(new FileResponse(uploadFileId));
        } catch (Exception e)
            {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        
            }     
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterLoanApplication([Required] [FromBody] RegisterLoanApplicationRequest request)
    {
        try {
            var loanApplicationId = await _loanapplicationCommands.RegisterLoanAsync(request.Name, request.Email, request.Amount);
            return Ok(new NewLoanApplicationResponse(loanApplicationId));
        }   
        catch (Exception e)
        {   
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
        var loanApplication = await _loanapplicationQueries.GetLoanApplicationAsync(fileId);
        return Ok(new LoanApplicationResponse(loanApplication.Name, loanApplication.Email, loanApplication.Amount, loanApplication.Id));
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);  
        }
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<LoanApplicationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoanApplications()
    {
        try {
        var list = await _loanapplicationQueries.GetLoanApplicationsAsync();
        return Ok(list);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        
        }
    }
}