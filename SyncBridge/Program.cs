using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using SyncBridge.Data;

namespace SyncBridge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Env.Load();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<SyncBridgeDbContext>(options =>
     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
