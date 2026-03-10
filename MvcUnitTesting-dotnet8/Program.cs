using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Models;
using Tracker.WebAPIClient;

namespace MvcUnitTesting_dotnet8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRepository<Book>, WorkingBookRepository<Book>>();
            builder.Services.AddScoped<BookSeeder>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BookDbContext>();
                context.Database.Migrate();

                var seeder = scope.ServiceProvider.GetRequiredService<BookSeeder>();
                seeder.Seed();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            ActivityAPIClient.Track(
                StudentID: "S00198790",
                StudentName: "Ronan Keaveney",
                activityName: "Rad302 2026 Week 2 Lab 1",
                Task: "Implementing Production Repository Pattern");

            app.Run();
        }
    }
}