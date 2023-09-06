using Nordax.Bank.Recruitment.DataAccess.Entities;
using Nordax.Bank.Recruitment.DataAccess.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories
{
    public interface IUploadedFileRepository
    {
        Task<Guid> UploadFileAsync(byte[] file);
    }
    public class UploadedFileRepository : IUploadedFileRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UploadedFileRepository(IDbContextFactory dbContextFactory)
        {
            _applicationDbContext = dbContextFactory.Create();
        }

        public async Task<Guid> UploadFileAsync(byte[] data)
        {
            var newFile = _applicationDbContext.Add(new UploadedFile(data));
            await _applicationDbContext.SaveChangesAsync();

            return newFile.Entity.Id;
        }
    }
}
