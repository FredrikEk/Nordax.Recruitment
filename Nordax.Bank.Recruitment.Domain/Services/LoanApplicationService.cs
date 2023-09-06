using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.Domain.Services
{
    public interface ILoanApplicationService
    {
        Task<Guid> UploadFileAsync(byte[] data);
        Task<Guid> RegisterLoanApplicationAsync(Guid FileId);
        Task<LoanApplicationModel> GetLoanApplicationAsync(Guid loanApplicationId);
        IEnumerable<LoanApplicationModel> GetLoanApplications();
    }
    public class LoanApplicationService : ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _loanApplicationRepository;
        private readonly IUploadedFileRepository _uploadedFileRepository;
        public LoanApplicationService(ILoanApplicationRepository loanApplicationRepository, IUploadedFileRepository uploadedFileRepository)
        {
            _loanApplicationRepository = loanApplicationRepository;
            _uploadedFileRepository = uploadedFileRepository;
        }
        public Task<Guid> UploadFileAsync(byte[] data)
        {
            return _uploadedFileRepository.UploadFileAsync(data);
        }
        public Task<Guid> RegisterLoanApplicationAsync(Guid fileId)
        {
            return _loanApplicationRepository.RegisterLoanApplicationAsync(fileId);
        }
        public Task<LoanApplicationModel> GetLoanApplicationAsync(Guid fileId)
        {
            return _loanApplicationRepository.GetLoanApplicationAsync(fileId);
        }

        public IEnumerable<LoanApplicationModel> GetLoanApplications()
        {
            return _loanApplicationRepository.GetLoanApplications();
        }
    }
}
