using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nordax.Bank.Recruitment.DataAccess.Entities;
using Nordax.Bank.Recruitment.DataAccess.Exceptions;
using Nordax.Bank.Recruitment.Domain.Interfaces.Repositories;
using Nordax.Bank.Recruitment.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Nordax.Bank.Recruitment.DataAccess.Repositories;

public class LoanRepository : ILoanRepository
{
  private readonly ApplicationDbContext _applicationDbContext;

  public LoanRepository(ApplicationDbContext applicationDbContext)
  {
    _applicationDbContext = applicationDbContext;
  }

  public async Task<Guid> RegisterLoanAsync(string name, string email, int amount)
  {
    var newLoanApplication = await _applicationDbContext.Loans.AddAsync(new Loan(name, email, amount));
    await _applicationDbContext.SaveChangesAsync();

    return newLoanApplication.Entity.Id;
  }

  public async Task<LoanModel> GetLoanApplication(Guid loanApplicationId)
  {
    var loanApplication = await _applicationDbContext.Loans.FirstOrDefaultAsync(l => l.Id == loanApplicationId);
    if (loanApplication == null) throw new LoanApplicationException();
    return loanApplication.ToDomainModel();
  }

  public async Task<List<LoanModel>> GetLoanApplications()
  {
    var loans = await _applicationDbContext.Loans.ToListAsync();

    var loanModels = loans.Select(loan => new LoanModel(loan.Id, loan.Name, loan.Email, loan.Amount)).ToList();

    return loanModels;
  }

  public async Task<Guid> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded.");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var fileRecord = new FileRecord
        {
            Id = Guid.NewGuid(),
            FileName = file.FileName,
            FileType = file.ContentType,
            FileData = memoryStream.ToArray()
        };

        _applicationDbContext.FileRecords.Add(fileRecord);
        await _applicationDbContext.SaveChangesAsync();

        return fileRecord.Id;
    }
}