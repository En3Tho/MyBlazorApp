static class Extensions
{
    public static WebApplication UseStaticWasmEnvFile(this WebApplication app)
    {
        app.UseStaticFiles(new StaticFileOptions()
        {
            RequestPath = "/wasm",
            FileProvider = new InMemoryFileProvider(),
            ContentTypeProvider = new InMemoryContentTypeProvider(),
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
            }
        });
        
        return app;
    }
}