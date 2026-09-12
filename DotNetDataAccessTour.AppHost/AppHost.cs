var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebDataDemo>("webdatademo");

builder.Build().Run();
