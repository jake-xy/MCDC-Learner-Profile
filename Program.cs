using LearnerProfile.app.Data;
using LearnerProfile.app.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connString = "Data Source=LearnerProfile.db";
builder.Services.AddSqlite<LearnerProfileContext>(connString); // dependency injection

// add "cookie session" authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

var app = builder.Build();

// seeding the admin account
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LearnerProfileContext>();

    // ensure the database is created and up to date with migrations
    context.Database.Migrate();

    // check if an admin account already exists
    if (!context.Users.Any(u => u.Role == UserRole.Admin))
    {
        var defaultAdmin = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@learnerprofile.app",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), 
            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Users.Add(defaultAdmin);
        context.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
