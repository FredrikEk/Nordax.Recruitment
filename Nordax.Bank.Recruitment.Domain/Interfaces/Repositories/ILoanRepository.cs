using Nordax.Bank.Recruitment.Domain.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;



public interface ILoanRepository
{
  Task<Guid> RegisterLoanAsync(string name, string email, int amount);
  Task<LoanModel> GetLoanApplication(Guid loanApplicationId);
  Task<List<LoanModel>>GetLoanApplications();
  Task <Guid>UploadFile(IFormFile file);
}