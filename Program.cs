using KommProv.Archiver.Server.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor + Blazor aktivieren
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// EF DB-Context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Service registrieren
builder.Services.AddScoped<RuleArchivingService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
