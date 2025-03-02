using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.UI.Services;
using DotNetEnv;

using SneakerAPI.Api.Controllers.ProductControllers;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Infrastructure.Data;
using SneakerAPI.Infrastructure.Repositories;
using System.Text;
using SneakerAPI.Core.Models;
using SneakerAPI.Core.DTOs;
using StackExchange.Redis;
var  AllowHostSpecifiOrigins = "_allowHostSpecifiOrigins";
var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// var config=builder.Configuration;
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowHostSpecifiOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500").AllowAnyMethod()
                                                                            .AllowAnyHeader()
                                                                            .AllowCredentials();
                      });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<SneakerAPIDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SneakerAPIConnection"),b=>b.MigrationsAssembly("SneakerAPI.AdminApi")));



builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddTransient<IEmailSender,EmailSender>();
builder.Services.AddIdentity<IdentityAccount, IdentityRole<int>>()
    .AddEntityFrameworkStores<SneakerAPIDbContext>()
    .AddDefaultTokenProviders();
//*************** Tất cả config**
var config=builder.Configuration;
config["ConnectionStrings:SneakerAPIConnection"]=Environment.GetEnvironmentVariable("ConnectionString");
config["EmailSettings:SmtpServer"]=Environment.GetEnvironmentVariable("ES__SmtpServer");
config["EmailSettings:SmtpPort"]=Environment.GetEnvironmentVariable("ES__SmtpPort");
config["EmailSettings:SmtpUser"]=Environment.GetEnvironmentVariable("ES__SmtpUser");
config["EmailSettings:SmtpPass"]=Environment.GetEnvironmentVariable("ES__SmtpPass");

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
// Cấu hình Authentication với JWT
// builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
// {
//     options.TokenLifespan = TimeSpan.FromMinutes(5); // Token chỉ có hiệu lực trong 5 phút
// });
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = Environment.GetEnvironmentVariable("JWT__ValidIssuer"),
         ValidAudience = Environment.GetEnvironmentVariable("JWT__ValidAudience"),
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT__Secret")))
    };
});
//End Cònig
builder.Services.AddMemoryCache(); // Thêm dịch vụ MemoryCache
builder.Services.AddSession(); // Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache(); // Cần thiết cho Session
// System.Console.WriteLine(config["JWT:ValidIssuer"]);
// System.Console.WriteLine(config["JWT:ValidAudience"]);
// System.Console.WriteLine(config["JWT:Secret"]);
// System.Console.WriteLine(config["EmailSettings:SmtpServer"]);
// System.Console.WriteLine(config["EmailSettings:SmtpPort"]);
// System.Console.WriteLine(config["EmailSettings:SmtpUser"]);
// System.Console.WriteLine(config["EmailSettings:SmtpPass"]);
// System.Console.WriteLine(config["ConnectionStrings:SneakerAPIConnection"]);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(AllowHostSpecifiOrigins);
// Sử dụng Authentication và Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

app.Run();

