using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Services
{
    public interface ILoanApplicationService
    {
        Task UploadFileAsync();
        Task<Guid> RegisterLoanApplicationAsync(string name, string emailAddress);
        Task<LoanApplicationModel> GetLoanApplicationAsync(Guid subscriberId);
        Task<IEnumerable<LoanApplicationModel>> GetLoanApplicationsAsync();
    }
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _loanApplicatioRepository;
        public LoanApplicationService()
        {

        }
        public Task UploadFileAsync()
        {
            throw new NotImplementedException();
        }
        public Task<Guid> RegisterLoanApplicationAsync(string name, string emailAddress)
        {
            throw new NotImplementedException();
        }
        public Task<LoanApplicationModel> GetLoanApplicationAsync(Guid subscriberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoanApplicationModel>> GetLoanApplicationsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
