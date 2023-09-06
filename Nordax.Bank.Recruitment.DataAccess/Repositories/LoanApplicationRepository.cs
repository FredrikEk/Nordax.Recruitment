using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories
{
    public interface ILoanApplicationRepository
    {
        Task UploadFileAsync();
        Task<Guid> RegisterLoanApplicationAsync(string name, string emailAddress);
        Task<LoanApplicationModel> GetLoanApplicationAsync(Guid subscriberId);
        Task<IEnumerable<LoanApplicationModel>> GetLoanApplicationsAsync();
    }
    public class LoanApplicationRepository
    {
    }
}
