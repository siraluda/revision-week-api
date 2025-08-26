using Domain.AppUser;
using Domain.Common;

namespace Domain.Documents;

public sealed class Document : BaseEntity
{
    public User Owner { get; private set; }
    public string FileName { get; private set; }
    public long FileSize { get; private set; }
    public string Key { get; private set; }
    public string ContentType { get; private set; }
    private const long BytesScale = 1024 * 1024;
    private const long MaximumFileSize = 1000 * BytesScale; // 1 GB
    
    private Document(){}

    public Document(string fileName, long fileSize, string contentType, string key, User owner)
    {
        if (string.IsNullOrWhiteSpace(fileName)) throw new DomainException("File name cannot be empty.");
        if (string.IsNullOrWhiteSpace(key)) throw new DomainException("Key cannot be empty.");
        if (string.IsNullOrWhiteSpace(contentType)) throw new DomainException("Content type cannot be empty.");
        if (fileSize <= 0) throw new DomainException("File size must be greater than zero.");
        if (fileSize > MaximumFileSize) throw new DomainException(
            $"Standard users cannot upload files larger than {(int)(MaximumFileSize / BytesScale*1000)} GB");
        
        FileName = fileName;
        FileSize = fileSize;
        ContentType = contentType;
        Key = key;
        Owner = owner;
    }
    
    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName)) throw new DomainException("Name cannot be empty.");
        FileName = newName;
    }
}