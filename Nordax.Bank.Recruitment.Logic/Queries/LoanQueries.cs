using Nordax.Bank.Recruitment.Domain.Interfaces.Queries;
using Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
using Nordax.Bank.Recruitment.Domain.Models;

namespace Nordax.Bank.Recruitment.Logic.Queries;

public class LoanQueries : ILoanQueries
{
    private readonly ILoanRepository _loanRepository;

    public LoanQueries(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<LoanModel> GetLoanApplicationAsync(Guid loanApplicationId)
    {
        var loanApplication = await _loanRepository.GetLoanApplication(loanApplicationId);
        return loanApplication;
    }

    public async Task<List<LoanModel>> GetLoanApplicationsAsync()
    {
        var loanApplicationsList = await _loanRepository.GetLoanApplications();
        return loanApplicationsList;
    }
}