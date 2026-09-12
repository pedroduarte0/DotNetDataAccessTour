var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithDataVolume();

var database = sql.AddDatabase("DefaultConnection", "DotNetDataAccessTour");

var webDataDemo = builder.AddProject<Projects.WebDataDemo>("webdatademo")
    .WithReference(database)
    .WaitFor(database);

var webDataDemoMigrations = webDataDemo
    .AddEFMigrations("webdatademo-migrations", "WebDataDemo.Data.AppDbContext")
    .RunDatabaseUpdateOnStart();

webDataDemo.WaitForCompletion(webDataDemoMigrations);

builder.Build().Run();
