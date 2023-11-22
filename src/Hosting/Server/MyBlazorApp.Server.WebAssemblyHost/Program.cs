using System.Text.Json;
using En3Tho.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Json;
using MyBlazorApp.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.AddServerDefaults();
builder.ConfigureServerOpenTelemetry(new(ServiceName: "Wasm host"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization();
builder.Services.AddCors(o =>
    o.AddDefaultPolicy(builder =>
        builder
            .SetIsOriginAllowed(_ => true)
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services.ConfigureHttpJsonOptions(options =>
    Json.AddFSharpConverters(options.SerializerOptions));

builder.Services.AddOrReplaceSingleton<JsonSerializerOptions>(serviceProvider =>
    serviceProvider.GetRequiredService<JsonOptions>().SerializerOptions);

var app = builder.Build();

app.UseCors();
app.UseAuthorization();
app.UseHttpsRedirection();

//app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

//app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Run();