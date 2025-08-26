using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RevisionWeek.API.Contracts;

namespace RevisionWeek.API.Extensions;

public static class DocumentsRouteGroup
{
    public static void RegisterDocumentsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/documents");
        
        group.MapGet("/", GetAllDocuments);
        group.MapPost("/", CreateDocuments);
        group.MapGet("/{id}", GetDocument);
        group.MapPut("/{id}", UpdateDocuments);
        group.MapDelete("/{id}", DeleteDocuments);
    }
    
    //Handlers
    public static async Task<Results<Ok<AllDocuments>, NotFound<Error>>> GetAllDocuments()
    {
        var result = new AllDocuments(new List<Document>
        {
            new Document(
                "asd-2323-adaaf", 
                "Document Name", 
                "pdf", 
                "document-key", 
                DateTime.UtcNow),
            
            new Document(
                "asd-2323-adaaf",
                "Document Name 2", 
                "pdf", 
                "document-key-2",
                DateTime.UtcNow),
        });
        return result.items.Count > 0 ? 
            TypedResults.Ok(result) : 
            TypedResults.NotFound(new Error("NOT_FOUND", "Cannot find document"));
    }
    
    public static async Task<Results<Ok<Document>, NotFound<Error>>> GetDocument(string id)
    {
        return TypedResults.Ok(new Document(
            "asd-2323-adaaf",
            "Document Name 3",
            "pdf",
            "document-key-3", 
            DateTime.UtcNow));
    }
    
    public static async Task<Results<Ok<Document>, ValidationProblem>> CreateDocuments(CreateUpdateDocument document)
    {
        return TypedResults.Ok(new Document(
            "asd-2323-adaaf",
            "Document Name 3",
            "pdf",
            "document-key-3",
            DateTime.UtcNow));
    }
    
    public static async Task<Results<Ok<Document>, NotFound<Error>>> UpdateDocuments(CreateUpdateDocument document)
    {
        return TypedResults.Ok(
            new Document(
                "asd-2323-adaaf",
                "Document Name 3",
                "pdf",
                "document-key-3",
                DateTime.UtcNow));
    }
    
    public static async Task<Results<NoContent, NotFound<Error>>> DeleteDocuments(string id)
    {
        return TypedResults.NoContent();
    }
}