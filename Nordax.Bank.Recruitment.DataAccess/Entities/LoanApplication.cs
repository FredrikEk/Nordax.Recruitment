using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nordax.Bank.Recruitment.DataAccess.Entities
{
    public class LoanApplication
    {
        public Guid Id { get; set; }
        public UploadedFile UploadedFile { get; set; }
        public LoanApplicationModel ToDomainModel()
        {
            return new LoanApplicationModel()
            {
                Id = Id,
                UploadedFile = UploadedFile.ToDomainModel()
            };
        }
    }
}
