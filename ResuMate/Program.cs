using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;
using ResuMate.Components;
using ResuMate.Components.Account;
using ResuMate.Data;
using ResuMate.Services;
using ResuMate.Services.CvServices;
using ResuMate.Services.PersonalLetterServices;
using ResuMate.Shared.Models;

namespace ResuMate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddHttpClient();

        var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(apiBaseUrl)
        });

        QuestPDF.Settings.License = LicenseType.Community;

        builder.Services.AddSingleton<CvModel>();
        builder.Services.AddSingleton<EducationModel>();
        builder.Services.AddSingleton<ExperienceModel>();
        builder.Services.AddSingleton<ReferenceModel>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
            .AddIdentityCookies();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Register DbContext with scoped factory
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register DbContextFactory with scoped lifetime
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddScoped<CreateCvPdfService>();
        builder.Services.AddScoped<CreatePersonalLetterPdfService>();
        builder.Services.AddScoped<GetCvService>();
        builder.Services.AddScoped<GetPersonalLetterService>();
        builder.Services.AddScoped<DeleteCvService>();
        builder.Services.AddScoped<DeletePersonalLetterService>();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        // Ensure UserManager is available to resolve dependencies
        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        app.MapAdditionalIdentityEndpoints();

        try
        {
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Application failed to start: {ex.Message}");
            throw;
        }
    }
}
