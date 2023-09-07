using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.Domain.Services;

namespace Nordax.Bank.Recruitment.Doman.Tests.ServiceTests
{
    [TestClass]
    public class LoanApplicationServiceTests
    {
        private readonly Mock<ILoanApplicationRepository> _loanApplicationRepositoryMock;
        private readonly Mock<IUploadedFileRepository> _uploadedFileRepositoryMock;
        private readonly ILoanApplicationService _loanApplicationService;

        public LoanApplicationServiceTests()
        {
            _loanApplicationRepositoryMock = new Mock<ILoanApplicationRepository>();
            _uploadedFileRepositoryMock = new Mock<IUploadedFileRepository>();
            _loanApplicationService = new LoanApplicationService(_loanApplicationRepositoryMock.Object, _uploadedFileRepositoryMock.Object);
        }

        [TestMethod]
        public async Task UploadFileAsync_NoErrors_VerifyCalls()
        {
            var fileId = await _loanApplicationService.UploadFileAsync(new byte[2]);

            _uploadedFileRepositoryMock.Verify(a => a.UploadFileAsync(new byte[2]));
        }

        [TestMethod]
        public async Task RegisterLoanApplicationAsync_NoErrors_VerifyCalls()
        {
            var fileId = await _loanApplicationService.UploadFileAsync(new byte[2]);
            await _loanApplicationService.RegisterLoanApplicationAsync(fileId);

            _loanApplicationRepositoryMock.Verify(a => a.RegisterLoanApplicationAsync(fileId));
        }
    }
}