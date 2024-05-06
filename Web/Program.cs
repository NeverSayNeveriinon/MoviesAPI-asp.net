using Entities;
using Microsoft.EntityFrameworkCore;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // DataBase IOC
        var DBconnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>
        (options =>
        {
            options.UseSqlServer(DBconnectionString);
        });
        
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}