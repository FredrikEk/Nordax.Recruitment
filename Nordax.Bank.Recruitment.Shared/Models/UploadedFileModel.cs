using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Shared.Models
{
    public class UploadedFileModel
    {
        public Guid Id { get; set; }
        public Byte[] Data { get; set; }
        public LoanApplicationModel LoanApplication { get; set; }
    }
}
