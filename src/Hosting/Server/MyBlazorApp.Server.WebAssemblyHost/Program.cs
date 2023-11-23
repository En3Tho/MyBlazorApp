var builder = WebApplication.CreateBuilder(args);

builder.AddServerDefaults();
builder.ConfigureServerOpenTelemetry(new(ServiceName: "Wasm host"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthorization();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStaticWasmEnvFile();

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