namespace RevisionWeek.API.Contracts;

public record Error(string code, string message);
public record DetailedErrors(string code, string message, string[] details);