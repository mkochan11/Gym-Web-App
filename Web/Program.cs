using System;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Configuration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices(builder.Configuration);
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymAppDb")));
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymAppIdentityDb")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var indentityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var UsersIds = await AppIdentityDbContextSeed.SeedAsync(indentityContext, userManager, roleManager);

        var appDbContext = scopedProvider.GetRequiredService<AppDbContext>();
        await AppDbContextSeed.SeedAsync(appDbContext, UsersIds);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while seeding DB");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
