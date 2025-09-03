
using Microsoft.Extensions.DependencyInjection;
using SmartChat.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SmartChatDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapperService();

builder.Services.AddRepositoryService();

builder.Services.AddRondominterfacesService();


builder.Services.AddCustomMidMediatorServiceOfUser();

builder.Services.AddCustomMidMediatorServiceOfRole();

builder.Services.AddCustomMidMediatorServiceOfMessage();

builder.Services.AddCustomMidMediatorServiceOfConversation();

builder.Services.AddCustomMidMediatorServiceOfTypingStatues();

builder.Services.AddCustomMidMediatorServiceOfLoginUse();

builder.Services.AddSignalR();

builder.Services.AddDistributedMemoryCache(); 

builder.Services.AddControllersWithViews();

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });
var app = builder.Build();

app.UseSession();
// Configure the HTTP request pipeline.
app.MapGet("/", context => {
    context.Response.Redirect("/Auth/Login");
    return Task.CompletedTask;
});

app.MapHub<ChatHub>("/chatHub");

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
