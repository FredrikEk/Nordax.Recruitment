using Nordax.Bank.Recruitment.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
public interface ILoanApplicationRepository
{
    Task<Guid> UploadFileAsync(string fileName, string contentType, byte[] content);
    Task<Guid> RegisterLoanApplicationAsync(string name, Guid fileId, string description);
    Task<LoanApplicationModel> GetLoanApplication(Guid loanApplicationId);
    Task<List<LoanApplicationModel>> GetLoanApplications();
}

