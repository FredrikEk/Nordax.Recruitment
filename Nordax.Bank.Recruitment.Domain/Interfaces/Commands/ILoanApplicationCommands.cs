using System.Threading.Tasks;
using System;

namespace Nordax.Bank.Recruitment.Domain.Interfaces.Commands;
public interface ILoanApplicationCommands
{
    Task<Guid> RegisterLoanApplicationAsync(string name, string description);
}