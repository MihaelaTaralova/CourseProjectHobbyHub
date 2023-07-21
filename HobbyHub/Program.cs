using HobbyBubSystem.Data.Models.Account;
using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HobbyHubDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<HobbyUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity: SignIn: RequireConfirmedAccount");
    options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity: Password: RequireDigit");
    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity: Password: RequireUppercase");
    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity: Password: RequireLowercase");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity: Password: RequireNonAlphanumeric");
    options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity: Password: RequiredLength");
    options.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity: User: RequireUniqueEmail");
    options.User.AllowedUserNameCharacters = builder.Configuration.GetValue<string>("Identity: User: AllowedUserNameCharacters");

})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<HobbyHubDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IHobbyService, HobbyService>();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
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
app.MapRazorPages();

app.Run();
