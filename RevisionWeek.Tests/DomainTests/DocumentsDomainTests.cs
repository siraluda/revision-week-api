using Domain.AppUser;
using Domain.Common;
using Domain.Documents;

namespace RevisionWeek.Tests.DomainTests;

public class DocumentsDomainTests
{
    private readonly Document _document;
    private readonly User _user;
    
    public DocumentsDomainTests()
    {
        string fileName = "fileName";
        long fileSize = 23400;
        string contentType = "application/pdf";
        string key = "key";
        _user = new User("id", "test@email.com", "TestUser") ;
        
        _document = new Document(fileName, fileSize, contentType, key, _user);
    }
    
    [Fact]
    public void initialize_document_with_correct_params_should_create_document_object()
    {
        // Arrange
        string fileName = "fileName";
        long fileSize = 23400;
        string contentType = "application/pdf";
        string key = "key";
        
        // Act
        // Assert
        Assert.NotNull(_document);
        Assert.Equal(fileName, _document.FileName);
        Assert.Equal(fileSize, _document.FileSize);
        Assert.Equal(contentType, _document.ContentType);
        Assert.Equal(key, _document.Key);
        Assert.IsType<Document>(_document);
    }

    [Fact]
    public void creating_document_without_fileName_should_throw_exception()
    {
        // Arrange
        string fileName = "";
        long fileSize = 23400;
        string contentType = "application/pdf";
        string key = "key";
        User user = _user;
        
        // Assert
        Assert.Throws<DomainException>(() => new Document(fileName, fileSize, contentType, key, user));
    }
    
    [Fact]
    public void creating_document_without_key_should_throw_exception()
    {
        // Arrange
        string fileName = "fileName";
        long fileSize = 23400;
        string contentType = "application/pdf";
        string key = "";
        User user = _user;
        
        // Assert
        Assert.Throws<DomainException>(() => new Document(fileName, fileSize, contentType, key, user));
    }

    [Fact]
    public void creating_document_without_contentType_should_throw_exception()
    {
        // Arrange
        string fileName = "fileName";
        long fileSize = 23400;
        string contentType = "";
        string key = "key";
        User user = _user;
        
        // Assert
        Assert.Throws<DomainException>(() => new Document(fileName, fileSize, contentType, key, user));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void creating_document_with_fileSize_less_than_or_equal_to_zero_should_throw_exception(int fileSizeInput)
    {
        string fileName = "fileName";
        long fileSize = fileSizeInput;
        string contentType = "application/pdf";
        string key = "key";
        User user = _user;
        
        Assert.Throws<DomainException>(() => new Document(fileName, fileSize, contentType, key, user));
    }
    
    [Theory]
    [InlineData(unchecked(1000*1024*1024*1024))] // 1 GB file size
    public void creating_document_with_fileSize_exceeding_maximum_size_should_throw_exception(long fileSizeInput)
    {
        string fileName = "fileName";
        long fileSize = fileSizeInput;
        string contentType = "application/pdf";
        string key = "key";
        User user = _user;
        
        Assert.Throws<DomainException>(() => new Document(fileName, fileSize, contentType, key, user));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void renaming_a_document_without_a_valid_string_name_should_throw_exception(string fileName)
    {
        Assert.Throws<DomainException>(() => _document.Rename(fileName));
    }

    [Theory]
    [InlineData("newName")]
    public void renaming_a_document_with_a_valid_string_name_should_succeed(string newName)
    {
        _document.Rename(newName);
        Assert.Equal(newName, _document.FileName);
    }
}