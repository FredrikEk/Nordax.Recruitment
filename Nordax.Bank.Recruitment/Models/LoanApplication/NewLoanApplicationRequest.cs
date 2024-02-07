using System;
using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.Models.LoanApplication;

public record NewLoanApplicationRequest(
    [Required]string Name, 
    Guid FileId,
    string Description
);