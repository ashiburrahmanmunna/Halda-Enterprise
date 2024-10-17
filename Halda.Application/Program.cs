using Autofac;
using Autofac.Extensions.DependencyInjection;
using Halda.Application.Handler;
using Halda.Application.Services;
using Halda.Core.Converter;
using Halda.DataAccess.Middleware;
using Halda.DataAccess.Persistence;
using Halda.DataAccess.Repositories;
using Halda.DataAccess.Services.Interface.ICompany;
using Halda.DataAccess.Services.Repository.Company;
using Halda.Utilities.FileUpload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;





var builder = WebApplication.CreateBuilder(args);

//var builder = WebApplication.CreateBuilder();

// Add Razor Pages with runtime compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddDbContext<HaldaDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseLowerCaseNamingConvention());


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Chitra").AddCookie("Chitra");



builder.Services.AddCors(p => p.AddPolicy("DomainPolicy", option =>
{
    option.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();



builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<HttpClientAuthorizationHandler>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddSingleton<IFileService, FileService>();

//builder.Services.AddHttpClient("Halda", c =>
//{

//    c.BaseAddress = new Uri("https://localhost:7008/api/v1/"); // Development
//    //c.BaseAddress = new Uri("http://localhost:5016/"); // Development
//    //c.BaseAddress = new Uri("http://localhost:8080/api/v1/");   // Local IIS
//})
//.AddHttpMessageHandler<HttpClientAuthorizationHandler>();

builder.Services.AddHttpClient("Chitra", c =>
{
    c.BaseAddress = new Uri("http://gtrbd.net/chitraupdateapi/api/");
});
//.AddHttpMessageHandler<HttpClientAuthorizationHandler>();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseStaticFiles();

app.UseRouting();
app.UseSession();
//app.UseMiddleware<CompanyIdMiddleware>();
app.UseCors("DomainPolicy");

app.UseAuthentication();
app.UseAuthorization();

#pragma warning disable ASP0014 
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}"
 );
});
#pragma warning restore ASP0014


app.Run();
