using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
using Nordax.Bank.Recruitment.Domain.Models;
using System.Threading.Tasks;
using System;
using Nordax.Bank.Recruitment.DataAccess.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories;
public class LoanApplicationRepository : ILoanApplicationRepository
{

    private readonly ApplicationDbContext _applicationDbContext;

    public LoanApplicationRepository(ApplicationDbContext applicationDbContext)
    {
            _applicationDbContext = applicationDbContext;
    }
    public async Task<Guid> RegisterLoanApplicationAsync(string name, string description)
    {
       if (await _applicationDbContext.LoanApplications.AnyAsync(la => la.Name == name))
            throw new LoanApplicationPendingException();

        var newLoanApplication = await _applicationDbContext.LoanApplications.AddAsync(new LoanApplication(name, description));
        await _applicationDbContext.SaveChangesAsync();

        return newLoanApplication.Entity.Id;
    }
    public async Task<LoanApplicationModel> GetLoanApplication(Guid fileId)
    {
        var loanApplication = await _applicationDbContext.LoanApplications.FirstOrDefaultAsync(la => la.Id == fileId);
        if (loanApplication == null) throw new LoanApplicationNotFoundException();
        return loanApplication.ToDomainModel();
    }
    public async Task<List<LoanApplicationModel>> GetLoanApplications()
    {
        var loanApplicationDomainModels = await _applicationDbContext.LoanApplications.Select(la => la.ToDomainModel()).ToListAsync();
        if (loanApplicationDomainModels.Count < 1) throw new LoanApplicationNotFoundException();
        
        return loanApplicationDomainModels;
    }
}

