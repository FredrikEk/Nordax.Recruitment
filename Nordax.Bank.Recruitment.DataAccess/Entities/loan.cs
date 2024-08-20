using System;
using System.ComponentModel.DataAnnotations;
using Nordax.Bank.Recruitment.Domain.Models;

namespace Nordax.Bank.Recruitment.DataAccess.Entities;

public sealed class Loan
{
  public Loan()
  {

  }
  public Loan(string name, string email, int amount)
  {
    Name = name;
    Email = email;
    Amount = amount;
  }
  
  public Guid Id { get; set; }

    [Required] [MaxLength(200)] public string Name { get; set; }

    [Required] [MaxLength(200)] public string Email { get; set; }

    [Required] public int Amount { get; set; }

    public LoanModel ToDomainModel() => new(Id, Name, Email, Amount);
}