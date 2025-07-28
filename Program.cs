using KommProv.Archiver.Server.Data;
using KommProv.Archiver.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor + Blazor aktivieren
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// EF DB-Context
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.CommandTimeout(300); // Set 5-minute timeout for EF commands
    }));

// App-Einstellungen (LoginSettings)
builder.Services.Configure<LoginSettings>(
    builder.Configuration.GetSection("LoginSettings"));

// Session + HTTP-Zugriff
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Dienste registrieren
builder.Services.AddScoped<UserSessionService>();

builder.Services.AddScoped<RuleArchivingService>();

var app = builder.Build();

// Fehlerseiten in Produktion
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // ← optional für HTTP deaktivieren
}

// Optional: HTTPS deaktivieren (für lokal HTTP)
app.UseHttpsRedirection(); // ← ⚠️ REMOVE or COMMENT if you want only HTTP

// statische Dateien
app.UseStaticFiles();

// Routing
app.UseRouting();

// ✅ Session aktivieren
app.UseSession();

// Endpunkte
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
