using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnboardingApp.Components;
using OnboardingApp.Data;
using OnboardingApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddRazorPages();

// DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
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
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

// Authorization
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Create roles at startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "Employee" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // CREATE ADMIN USER FOR TESTING
    string adminEmail = "admin@test.se";
    string adminPassword = "Password123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }
        }
    }

    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // CREATE EMPLOYEE USER FOR TESTING
    string employeeEmail = "employee@test.se";
    string employeePassword = "Password123!";

    var employeeUser = await userManager.FindByEmailAsync(employeeEmail);

    if (employeeUser == null)
    {
        employeeUser = new ApplicationUser
        {
            UserName = employeeEmail,
            Email = employeeEmail,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(employeeUser, employeePassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
            }
        }
    }

    if (employeeUser != null && !await userManager.IsInRoleAsync(employeeUser, "Employee"))
    {
        await userManager.AddToRoleAsync(employeeUser, "Employee");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
