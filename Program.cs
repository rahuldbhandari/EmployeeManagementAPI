using EmployeeManagementAPI.BAL;
using EmployeeManagementAPI.BAL.IServices;
using EmployeeManagementAPI.DAL.DBContext;
using EmployeeManagementAPI.DAL.IRepositories;
using EmployeeManagementAPI.DAL.Repositories;
using EmployeeManagementAPI.Helper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB Connection
builder.Services.AddDbContext<EmployeeContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("EmployeeDBConnection")));

//REPOSITORIES
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

// SERVICES
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

// CONFIGS
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
