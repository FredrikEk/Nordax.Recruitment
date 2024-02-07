using System;

namespace Nordax.Bank.Recruitment.Models.LoanApplication;

public record LoanApplicationResponse(Guid Id, string Name, Guid FileId);