using Nordax.Bank.Recruitment.Domain.Interfaces.Commands;
using Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;

namespace Nordax.Bank.Recruitment.Logic.Commands;
public class LoanApplicationCommands : ILoanApplicationCommands
{
    private readonly ILoanApplicationRepository _loanApplicationRepository;

    public LoanApplicationCommands(ILoanApplicationRepository loanApplicationRepository)
    {
        _loanApplicationRepository = loanApplicationRepository;
    }

    public async Task<Guid> UploadFileAsync(string fileName, string contentType, byte[] content)
    {
        
        var fileId = await _loanApplicationRepository.UploadFileAsync(fileName, contentType, content);
        return fileId;
    }

    public async Task<Guid> RegisterLoanApplicationAsync(string name, Guid fileId, string description)
    {
        var loanApplicationId = await _loanApplicationRepository.RegisterLoanApplicationAsync(name, fileId, description);
        return loanApplicationId;
    }
}

