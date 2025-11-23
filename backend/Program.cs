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

        // Serve static files from wwwroot (production build)
        app.UseStaticFiles();

        app.UseRouting();
        app.MapControllers();

        // Fallback to index.html for SPA routing (when frontend is built to wwwroot)
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
