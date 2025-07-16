using KommProv.Archiver.Server.Data;
using KommProv.Archiver.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor + Blazor aktivieren
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// EF DB-Context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<LoginSettings>(
    builder.Configuration.GetSection("LoginSettings"));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();

// Service registrieren
builder.Services.AddScoped<RuleArchivingService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
