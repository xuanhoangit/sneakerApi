using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using SneakerAPI.AdminApi.Controllers.ProductControllers;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Infrastructure.Data;
using SneakerAPI.Infrastructure.Repositories;
using System.Text;
using SneakerAPI.Core.Models;
using DotNetEnv;
var  AllowHostSpecifiOrigins = "_allowHostSpecifiOrigins";
Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowHostSpecifiOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5173").AllowAnyMethod()
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

// Cấu hình Authentication với JWT
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
         ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
         ValidAudience = builder.Configuration["JWT:ValidAudience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT__Secret")))
    };
});

var app = builder.Build();
// Thực hiện seed Role và tài khoản Admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    try
    {
        await SeedRoleAdmin.CreateRoles(services);
    }
    catch(Exception ex)
    {
        // Log lỗi nếu cần
        Console.WriteLine("hahaha"+ex.Message);
    }
}

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

