using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServerDefaults();
builder.ConfigureServerOpenTelemetry(new());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization();

var app = builder.Build();

var webHostEnvironment = app.Services.GetRequiredService<IWebHostEnvironment>();

// modify appsettings.json with provided env variables for wasm
var fileInfo =
    await WasmEnvFile.CreateAppSettingsJson(webHostEnvironment.WebRootFileProvider.GetFileInfo("appsettings.json"));
webHostEnvironment.WebRootFileProvider = new WasmEnvFileProvider(webHostEnvironment.WebRootFileProvider, fileInfo);

app.UseAuthorization();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapRazorPages();
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