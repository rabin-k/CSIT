using CSITCommerce;
using CSITCommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<CommerceDbContext>(op =>
{
    op.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True;");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CommerceDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(op =>
{
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireLowercase = false;
    op.Password.RequireUppercase = false;
    op.Password.RequireDigit = false;

    op.User.RequireUniqueEmail = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
