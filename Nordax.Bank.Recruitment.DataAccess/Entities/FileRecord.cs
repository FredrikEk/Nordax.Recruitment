using System;

public class FileRecord
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public byte[] FileData { get; set; }
}