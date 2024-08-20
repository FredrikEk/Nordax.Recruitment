
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;



namespace Nordax.Bank.Recruitment.Domain.Interfaces.Commands;

public interface ILoanCommands
{
    Task<Guid> RegisterLoanAsync(string name, string email, int amount);
    Task<Guid> UploadFileAsync(IFormFile file);
}


