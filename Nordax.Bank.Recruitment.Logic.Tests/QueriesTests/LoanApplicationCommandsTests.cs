using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
using Nordax.Bank.Recruitment.Logic.Commands;

namespace Nordax.Bank.Recruitment.Logic.Tests.CommandsTests;

[TestClass]
public class LoanApplicationCommandsTests
{
    private readonly Mock<ILoanApplicationRepository> _loanApplicationRepositoryMock;
    private readonly LoanApplicationCommands _loanApplicationCommands;

    public LoanApplicationCommandsTests()
    {
        _loanApplicationRepositoryMock = new Mock<ILoanApplicationRepository>();
        _loanApplicationCommands = new LoanApplicationCommands(_loanApplicationRepositoryMock.Object);
    }

    [TestMethod]
    public async Task RegisterSubscriptionAsync_NoErrors_VerifyCalls()
    {
        
        var name = "firstName";
        var description = "nonEmptyString";
        await _loanApplicationCommands.RegisterLoanApplicationAsync(name, description);

        _loanApplicationRepositoryMock.Verify(a => a.RegisterLoanApplicationAsync(name, description));
    }
}