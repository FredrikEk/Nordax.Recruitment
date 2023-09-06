using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Shared.Models
{
    public class LoanApplicationModel
    {
        public Guid Id { get; set; }
        public UploadedFileModel? UploadedFile { get; set; }
    }
}
