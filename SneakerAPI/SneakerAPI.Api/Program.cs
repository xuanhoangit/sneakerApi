using Microsoft.EntityFrameworkCore;
using SneakerAPI.Core.Interfaces;
using SneakerAPI.Infrastructure.Data;
using SneakerAPI.Infrastructure.Repositories;

var  AllowHostSpecifiOrigins = "_allowHostSpecifiOrigins";

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("SneakerAPIConnection"),b => b.MigrationsAssembly("SneakerAPI.Api")));

builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
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
app.MapControllers(); 

app.Run();

