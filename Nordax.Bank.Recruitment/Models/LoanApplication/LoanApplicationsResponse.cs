using System.Collections.Generic;

namespace Nordax.Bank.Recruitment.Models.LoanApplication;
public record LoanApplicationsResponse(IEnumerable<LoanApplicationResponse> LoanApplicationResponses);

