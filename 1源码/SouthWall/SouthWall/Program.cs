using Microsoft.EntityFrameworkCore;
using SouthWall;

var builder = WebApplication.CreateBuilder(args);
// 从 appsettings.json 中获取连接字符串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SWDbContext>(options =>
    options.UseMySql(connectionString,
         new MySqlServerVersion(new Version()))
);
builder.Services.AddScoped<ITimesDBAccess, TimesDBAccess>();
builder.Services.AddScoped<IVideosDBAccess, VideosDBAccess>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITimesService, TimesService>();
builder.Services.AddScoped<IVideosService, VideosService>();
var app = builder.Build();
AppSettingsHelper.SetAppSettings(app.Configuration.GetSection("AppSettings"));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
