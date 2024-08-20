using System;

namespace Nordax.Bank.Recruitment.Domain.Models;

public record LoanModel(Guid Id, string Name, string Email, int Amount);
