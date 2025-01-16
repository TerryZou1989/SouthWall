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
builder.Services.AddScoped<IRequestLogsDBAccess, RequestLogsDBAccess>();
builder.Services.AddScoped<IIPInfosDBAccess, IPInfosDBAccess>();
builder.Services.AddScoped<IDatasDBAccess, DatasDBAccess>();
builder.Services.AddScoped<ITimesDBAccess, TimesDBAccess>();
builder.Services.AddScoped<IVideosDBAccess, VideosDBAccess>();
builder.Services.AddScoped<IAudiosDBAccess, AudiosDBAccess>();
builder.Services.AddScoped<IArticlesDBAccess, ArticlesDBAccess>();
builder.Services.AddScoped<IMessagesDBAccess, MessagesDBAccess>();
builder.Services.AddScoped<IWebSitesDBAccess, WebSitesDBAccess>();
builder.Services.AddScoped<IShiJusDBAccess, ShiJusDBAccess>();
builder.Services.AddScoped<ITouXiangsDBAccess, TouXiangsDBAccess>();
builder.Services.AddScoped<IMDBAccess, MDBAccess>();

builder.Services.AddScoped<IToolService, ToolService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRequestLogsService, RequestLogsService>();
builder.Services.AddScoped<IIPInfosService, IPInfosService>();
builder.Services.AddScoped<IDatasService, DatasService>();
builder.Services.AddScoped<ITimesService, TimesService>();
builder.Services.AddScoped<IVideosService, VideosService>();
builder.Services.AddScoped<IAudiosService, AudiosService>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IMessagesService, MessagesService>();
builder.Services.AddScoped<IWebSitesService, WebSitesService>();
builder.Services.AddScoped<IShiJusService, ShiJusService>();
builder.Services.AddScoped<ITouXiangsService, TouXiangsService>();
builder.Services.AddScoped<IStatisticsServiceService, StatisticsServiceService>();

var app = builder.Build();
AppSettingsHelper.SetAppSettings(app.Configuration.GetSection("AppSettings"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Blog/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseMiddleware<RequestLoggingMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Index}/{id?}");

app.Run();
