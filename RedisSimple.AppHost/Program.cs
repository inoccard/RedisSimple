var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RedisSimple>("redissimple");

builder.Build().Run();
