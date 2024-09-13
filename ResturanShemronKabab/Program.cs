using ResturanShemronKabab.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var str = builder.Configuration["RestaurantConnectionString"];

var strSecurity = builder.Configuration["SecurityRestaurantConnectionString"];


Restaurant.Bootstrap.RestaurantBootstraper.WireUp(builder.Services, str);

Security.BootStrap.SecurityBootstrap.WireUp(builder.Services, strSecurity);

builder.Services.AddHsts(options =>
{

    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);

});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account/Login");
        o.LogoutPath = new PathString("/Account/Logout");
        o.AccessDeniedPath = new PathString("/Account/Login");
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CustomAuthenticator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
