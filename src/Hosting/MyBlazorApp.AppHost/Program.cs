var builder = DistributedApplication.CreateBuilder(args);

// var apiservice = builder.AddProject<Projects.MyBlazorApp.AppHost_ApiService>("apiservice");
//
// builder.AddProject<Projects.MyBlazorApp.AppHost_Web>("webfrontend")
//     .WithReference(apiservice);

builder.Build().Run();