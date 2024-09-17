using ResturanShemronKabab.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Restaurant.Application;
using Restaurant.ApplicationServiceContract.Services;
using ResturanShemronKabab.Framwork.UI.Services;
using ResturanShemronKabab.Frawwork.UI;

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
		o.ExpireTimeSpan = TimeSpan.FromMinutes(1);
		o.SlidingExpiration = false; 
		o.Cookie.IsEssential = true; // ??????? ?? ????? ??????? ????? ?????
		o.Cookie.HttpOnly = true; // ?????? ????? ???????
	});
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(1); // ????? ???? ?????? ???? ?? 1 ?????
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
