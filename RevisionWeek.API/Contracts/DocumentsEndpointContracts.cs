namespace RevisionWeek.API.Contracts;

public record Document(string id, string fileName, string contentType, string key, DateTime createdAt);
public record CreateUpdateDocument(string fileName, string contentType, string key);
public record AllDocuments(ICollection<Document> items);