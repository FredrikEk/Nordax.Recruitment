using Nordax.Bank.Recruitment.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace Nordax.Bank.Recruitment.DataAccess.Entities;
public sealed class LoanApplication
{
    public LoanApplication()
    {
            
    }

    public LoanApplication(string name, Guid fileId, string description)
    {
        Name = name;
        FileId = fileId;
        Description = description;
    }

    public Guid Id { get; set; }

    [Required][MaxLength(200)] public string Name { get; set; }
    [Required] public Guid FileId { get; set; }
    [Required][MaxLength(250)] public string Description { get; set; }

    public LoanApplicationModel ToDomainModel() => new(Id, Name, FileId);
}

