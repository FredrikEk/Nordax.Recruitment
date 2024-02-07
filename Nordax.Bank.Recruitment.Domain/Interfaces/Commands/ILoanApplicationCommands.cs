using System.Threading.Tasks;
using System;

namespace Nordax.Bank.Recruitment.Domain.Interfaces.Commands;
public interface ILoanApplicationCommands
{
    Task<Guid> UploadFileAsync(string fileName, string contentType, byte[] content);
    Task<Guid> RegisterLoanApplicationAsync(string name, Guid fileId, string description);
}