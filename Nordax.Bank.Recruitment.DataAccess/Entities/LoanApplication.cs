using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Nordax.Bank.Recruitment.DataAccess.Entities
{
    public class LoanApplication
    {
        public LoanApplication() {}
        public LoanApplication(Guid fileId)
        {
            UploadedFileId = fileId;
        }
        public Guid Id { get; set; }
        public Guid UploadedFileId { get; set; }
        public UploadedFile? UploadedFile { get; set; }
        public LoanApplicationModel ToDomainModel()
        {
            return new LoanApplicationModel()
            {
                Id = Id,
                UploadedFile = new UploadedFileModel()
                {
                    Id = UploadedFileId,
                    Data = UploadedFile.Data
                }
            };
        }
    }
}
