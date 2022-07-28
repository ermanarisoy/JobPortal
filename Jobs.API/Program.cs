using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Jobs.API.Data;
using Jobs.API.Interface;
using Jobs.API.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJobContext, JobContext>();
//builder.Services.AddDbContext<JobsAPIContext>(options =>
//    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("JobsAPIContext") ?? throw new InvalidOperationException("Connection string 'JobsAPIContext' not found.")));

// Add services to the container.

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
