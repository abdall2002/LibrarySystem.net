
using Library.Models;
using Microsoft.Extensions.FileProviders;
using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using User = Library.Models.User;

public class Program
{
    public static int Main()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        builder.Services.AddDbContext<LibraryContext>(options =>
        {
            options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<LibraryContext>().AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Lockout.MaxFailedAccessAttempts = 2;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
            //options.SignIn.RequireConfirmedEmail = true;
        });
        builder.Services.Configure<DataProtectionTokenProviderOptions>
         (options =>
         {
             options.TokenLifespan = TimeSpan.FromMinutes(5);
         });
        builder.Services.AddControllersWithViews();
        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();
        // Routing pattern
        app.MapControllerRoute("main", "{controller=Home}/{action=Index}/{id?}");
        //app.MapControllers();
        app.UseStaticFiles(new StaticFileOptions()
        {
            RequestPath="/Content",
            FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Content"))
        });

        app.Run();
        return 0;
    }
}