namespace SearchApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Register application services
        builder.Services.AddSingleton<SearchApp.Services.DocumentService>();

        // Add CORS for development
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors();

        // Serve static files from frontend build (only in production)
        var frontendPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "frontend", "dist");
        if (Directory.Exists(frontendPath))
        {
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                        frontendPath
                    ),
                    RequestPath = "",
                }
            );

            // Fallback to index.html for SPA routing
            app.MapFallbackToFile(
                "index.html",
                new StaticFileOptions
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                        frontendPath
                    ),
                }
            );
        }

        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}
