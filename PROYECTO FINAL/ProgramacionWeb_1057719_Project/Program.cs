using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSession(s =>
{
    s.IdleTimeout = TimeSpan.FromMinutes(60);
    s.Cookie.Name = "ClasificacionPeliculas.Session";
    s.Cookie.Expiration = TimeSpan.FromMinutes(60);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins",
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.SameSite = SameSiteMode.None;
                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                   options.LoginPath = "/Login"; /*this option indicates where is the login page*/
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
