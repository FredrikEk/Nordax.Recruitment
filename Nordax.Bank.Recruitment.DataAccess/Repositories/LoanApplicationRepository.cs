using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
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
        
        Task<Guid> RegisterLoanApplicationAsync(Guid fileId);
        Task<LoanApplicationModel> GetLoanApplicationAsync(Guid subscriberId);
        IEnumerable<LoanApplicationModel> GetLoanApplications();
    }
    public class LoanApplicationRepository : ILoanApplicationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LoanApplicationRepository(IDbContextFactory dbContextFactory)
        {
            _applicationDbContext = dbContextFactory.Create();
        }
        public async Task<LoanApplicationModel> GetLoanApplicationAsync(Guid fileId)
        {
            var loanApplication = await _applicationDbContext.LoanApplications.Include(l => l.UploadedFile).FirstOrDefaultAsync(s => s.UploadedFile.Id == fileId);
            if (loanApplication == null) throw new LoanApplicationNotFoundException();
            return loanApplication.ToDomainModel();
        }

        public IEnumerable<LoanApplicationModel> GetLoanApplications()
        {
            var loanApplications = _applicationDbContext.LoanApplications.Include(l => l.UploadedFile).Select(loanApplication => loanApplication.ToDomainModel()).ToList();
            if (loanApplications == null) throw new LoanApplicationNotFoundException();
            return loanApplications;
        }

        public async Task<Guid> RegisterLoanApplicationAsync(Guid fileId)
        {
            var newLoanApplication = await _applicationDbContext.LoanApplications.AddAsync(new LoanApplication(fileId));
            await _applicationDbContext.SaveChangesAsync();

            return newLoanApplication.Entity.Id;
        }
    }
}
