using MyBlazorApp.Server.Shared;
using MyBlazorApp.Server.WebAssemblyHost;
using MyBlazorApp.Services.DiscriminatedUnions.Server.V1;
using MyBlazorApp.Services.WeatherForecasts.Server.V1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMyBlazorAppServer(builder.Configuration);
builder.Services.AddRazorPages();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Logging.ConfigureLogging(builder.Configuration);

var app = builder.Build();

app.MapDiscriminatedUnionsEndpoints();
app.MapWeatherForecastsServiceEndpoints();

app.UseCors();
app.UseAuthorization();
app.UseHttpsRedirection();

//app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

//app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.MapReverseProxy();

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