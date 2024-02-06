using Nordax.Bank.Recruitment.Domain.Interfaces.Queries;
using Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
using Nordax.Bank.Recruitment.Domain.Models;

namespace Nordax.Bank.Recruitment.Logic.Queries;
public class LoanApplicationQueries : ILoanApplicationQueries
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;

    public LoanApplicationQueries(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public async Task<LoanApplicationModel> GetLoanApplicationAsync(Guid fileId)
    {
        var loanApplication = await _loanApplicationRepository.GetLoanApplication(fileId);
        return loanApplication;
    }
    public async Task<List<LoanApplicationModel>> GetLoanApplicationsAsync()
    {
        var loanApplications = await _loanApplicationRepository.GetLoanApplications();
        return loanApplications;
    }
}

