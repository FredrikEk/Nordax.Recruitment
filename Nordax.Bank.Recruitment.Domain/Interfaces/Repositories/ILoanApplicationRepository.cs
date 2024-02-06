using Nordax.Bank.Recruitment.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
public interface ILoanApplicationRepository
{
    Task<Guid> RegisterLoanApplicationAsync(string name, string description);
    Task<LoanApplicationModel> GetLoanApplication(Guid fileId);
    Task<List<LoanApplicationModel>> GetLoanApplications();
}

