using System.ComponentModel.DataAnnotations;
using System;
using Nordax.Bank.Recruitment.Domain.Models;

namespace Nordax.Bank.Recruitment.DataAccess.Entities;
public sealed class File
{
    public File()
    {

    }

    public File(string name, string contentType, byte[] content)
    {
        Name = name;
        ContentType = contentType;
        Content = content;
    }

    public Guid Id { get; set; }

    [Required][MaxLength(200)] public string Name { get; set; }
    [Required][MaxLength(250)] public string ContentType { get; set; }
    [Required] public byte[] Content { get; set; }  

    public FileModel ToDomainModel() => new(Name, ContentType, Content );
}
