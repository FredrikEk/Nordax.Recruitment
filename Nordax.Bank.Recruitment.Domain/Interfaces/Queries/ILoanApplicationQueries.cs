using System.Threading.Tasks;
using System;
using Nordax.Bank.Recruitment.Domain.Models;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.Domain.Interfaces.Queries;

public interface ILoanQueries
{
    Task<LoanModel> GetLoanApplicationAsync(Guid loanApplicationId);
    Task<List<LoanModel>> GetLoanApplicationsAsync();
}