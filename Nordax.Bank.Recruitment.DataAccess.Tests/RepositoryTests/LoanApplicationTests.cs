using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nordax.Bank.Recruitment.DataAccess.Entities;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using Nordax.Bank.Recruitment.DataAccess.Repositories;
using Nordax.Bank.Recruitment.DataAccess.Tests.Configuration;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.RepositoryTests
{
    [TestClass]
    public class LoanApplicationRepositoryTests
    {
        private readonly ILoanApplicationRepository _loanApplicationRepository;
        private readonly ApplicationDbContext _testDbContext;

        public LoanApplicationRepositoryTests()
        {
            var dbContextFactoryMock = new Mock<IDbContextFactory>();

            _testDbContext = EfConfig.CreateInMemoryTestDbContext();
            dbContextFactoryMock.Setup(d => d.Create()).Returns(EfConfig.CreateInMemoryApplicationDbContext());

            _loanApplicationRepository = new LoanApplicationRepository(dbContextFactoryMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _testDbContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public async Task RegisterLoanApplicationAsync_ShouldAddLoanApplication()
        {
            var fileId = Guid.NewGuid();
            await _loanApplicationRepository.RegisterLoanApplicationAsync(fileId);

            var loanApplication = await _testDbContext.LoanApplications.SingleAsync();

            Assert.AreEqual(fileId, loanApplication.UploadedFileId);
        }
    }
}