var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("ms-sql")
  .WithLifetime(ContainerLifetime.Persistent)
  .AddDatabase("webdatademo-db");

var seq = builder.AddSeq("seq")
  .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.WebDataDemo>("webdatademo")
  .WithReference(sqlServer)
  .WaitFor(sqlServer)
  .WithReference(seq)
  .WaitFor(seq); 

builder.AddProject<Projects.MigrationService>("migrationservice")
  .WithReference(sqlServer)
  .WaitFor(sqlServer);

builder.Build().Run();
