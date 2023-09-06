using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Entities
{
    public class UploadedFile
    {
        public UploadedFile() {}
        public UploadedFile(byte[] data)
        {
            Data = data;
        }
        public Guid Id { get; set; }
        public Byte[] Data { get; set; }

        public LoanApplication? LoanApplication { get; set; }
        public UploadedFileModel ToDomainModel()
        {
            return new UploadedFileModel()
            {
                Id = Id,
                LoanApplication = LoanApplication.ToDomainModel(),
            };
        }
    }
}
