using Microsoft.EntityFrameworkCore;
using InventoryService.Core.Interfaces;
using InventoryService.Infrastructure.Data;
using InventoryService.Infrastructure.Repositories;
var  AllowHostSpecifiOrigins = "_allowHostSpecifiOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowHostSpecifiOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5173","http://localhost:5174").AllowAnyMethod()
                                                                            .AllowAnyHeader()
                                                                            .AllowCredentials();
                      });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<InventoryServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryServiceConnection"),b => b.MigrationsAssembly("InventoryService.Api")));

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

