using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Interfaces;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Repositories;
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
builder.Services.AddDbContext<PaymentServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentServiceConnection"),b => b.MigrationsAssembly("PaymentService.Api")));

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

