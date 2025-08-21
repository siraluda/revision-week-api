var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RevisionWeek_API>("revisionweek-api");

builder.Build().Run();