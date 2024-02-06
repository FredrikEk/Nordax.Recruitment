using Nordax.Bank.Recruitment.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.Domain.Interfaces.Queries;
public interface ILoanApplicationQueries
{
    Task<LoanApplicationModel> GetLoanApplicationAsync(Guid fileId);
    Task<List<LoanApplicationModel>> GetLoanApplicationsAsync();
}

