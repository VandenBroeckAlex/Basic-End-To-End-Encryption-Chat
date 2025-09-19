var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Stamp_Web>("web");

builder.Build().Run();
