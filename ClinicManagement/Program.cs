using ClinicManagement.Data;
using ClinicManagement.Helpers;
using ClinicManagement.Middleware;
using ClinicManagement.Models.Facilities;
using ClinicManagement.Models.Humans;
using ClinicManagement.Services;
using ClinicManagement.Services.Facilities;
using ClinicManagement.Services.Humans;
using ClinicManagement.Validators.Facilites;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add connection to the PostgreSQL DB
builder.Services.AddDbContext<ClinicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IService<CabinetType>, CabinetTypeService>();
builder.Services.AddScoped<IService<Cabinet>, CabinetService>();
builder.Services.AddScoped<ICabinetService, CabinetService>();

builder.Services.AddScoped<IService<Person>, PersonService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IJwtService, JwtService>();

//Add global model validator
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CabinetModelValidator>();
});

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();

    options.ViewLocationFormats.Add("/Views/Facilities/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Health/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Humans/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Info/{1}/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
    options.ViewLocationFormats.Add("/Views/Shared/_Navigation/{0}.cshtml");
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Jwt")
    .AddScheme<AuthenticationSchemeOptions, JwtAuthHandler>("Jwt", options => { });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

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

app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();  
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
