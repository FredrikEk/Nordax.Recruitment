using System;

namespace Nordax.Bank.Recruitment.Models.LoanApplication;

public record LoanApplicationResponse(string Name, string Email, int Amount, Guid LoanApplicationId);