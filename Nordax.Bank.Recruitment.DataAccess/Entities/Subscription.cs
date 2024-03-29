﻿using System;
using System.ComponentModel.DataAnnotations;
using Nordax.Bank.Recruitment.Domain.Models;

namespace Nordax.Bank.Recruitment.DataAccess.Entities;

public sealed class Subscription
{
    public Subscription()
    {
    }

    public Subscription(string name, string email)
    {
        Name = name;
        Email = email;
        SignUpDate = DateTime.Now;
    }

    public Guid Id { get; set; }

    [Required] [MaxLength(200)] public string Name { get; set; }

    [Required] [MaxLength(200)] public string Email { get; set; }

    public DateTime SignUpDate { get; set; }

    public SubscriberModel ToDomainModel() => new(Id, Name, Email, SignUpDate);
        
}