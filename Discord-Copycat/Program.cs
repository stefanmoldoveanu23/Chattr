using ClassLibrary.Helpers;
using ClassLibrary.Helpers.Extentions;
using ClassLibrary.Helpers.Hubs;
using ClassLibrary.Helpers.Middleware;
using ClassLibrary.Helpers.Seeders;
using Discord_Copycat.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DiscordContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options => options.AddPolicy(name: "DiscordOrigins",
    policy =>
    {
        policy.WithOrigins("https://localhost:44458").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    }));
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddSeeders();
builder.Services.AddUtils();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSignalR();

var app = builder.Build();
SeedData(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("DiscordOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.UseRouting();
app.UseMiddleware<JwtMiddleware>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.MapHub<ChatHub>("/chatHub");

app.Run();

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<UserSeeder>();
        service.SeedAdmin();
    }
}