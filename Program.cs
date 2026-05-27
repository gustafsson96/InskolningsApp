using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnboardingApp.Components;
using OnboardingApp.Data;
using OnboardingApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true;
    });
builder.Services.AddRazorPages();

// DbContext with SQLite (for dev)
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
// );

// DbContext with SQL Server
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<ApplicationDbContext>(sp =>
    sp.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext()
);

// Identity
builder
    .Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/AccessDenied";
});

// Authorization
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = feature?.Error;

        if (exception != null)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

            logger.LogError(exception, "Unhandled exception at path: {Path}", feature?.Path);
        }

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Ett serverfel inträffade.");
    });
});

// Create roles at startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    string[] roles = { "Admin", "Employee" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

// Logout route
app.MapPost(
    "/logout",
    async (HttpContext context) =>
    {
        await context.SignOutAsync(IdentityConstants.ApplicationScheme);
        return Results.Redirect("/Login");
    }
);

app.MapRazorPages();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
