using Nordax.Bank.Recruitment.Shared.Models;
using System;

namespace Nordax.Bank.Recruitment.Models.LoanApplication
{
	public class LoanApplicationResponse
	{
		public LoanApplicationResponse(Guid loanApplicationId)
		{
			Id = loanApplicationId;
		}

		public Guid Id{ get; set; }
	}
}
