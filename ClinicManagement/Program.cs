using ClinicManagement.Data;
using ClinicManagement.Models.Facilities;
using ClinicManagement.Services;
using ClinicManagement.Services.Facilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add connection to the PostgreSQL DB
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IService<CabinetType>, CabinetTypeService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


//Add global model validator
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidateModelFilter>();
})
.AddRazorOptions(options =>
{
    options.ViewLocationFormats.Clear();

    options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Auth/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Facilities/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
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

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
