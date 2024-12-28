var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("ms-sql")
  .WithLifetime(ContainerLifetime.Persistent)
  .AddDatabase("webdatademo-db");

builder.AddProject<Projects.WebDataDemo>("webdatademo")
  .WithReference(sqlServer)
  .WaitFor(sqlServer);

builder.Build().Run();
