using System.ComponentModel.DataAnnotations;

namespace Nordax.Bank.Recruitment.Models.LoanApplication;

public record RegisterLoanApplicationRequest(
    [Required]string Name, 
    string Description
);