
using Domain.Documents;

namespace Application.Interfaces;

public interface IStorageService
{
    public Task<string> GenerateUploadUrl(string fileName, string key);
    public Task<string> GenerateDownloadUrl(string fileName, string key);
    public Task<string> GenerateDeleteUrlAsync(string fileName, string key);
}