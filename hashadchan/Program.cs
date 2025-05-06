using Common.Dto;
using Mock;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfasces;
using Service.Services;
 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();

//builder.Services.AddScoped<IService<CandidateDto>, CanditateService>();
//builder.Services.AddScoped<IService<UserDto>, UserService>();
//builder.Services.AddScoped<IRepository<Candidate>, CandidateRepository>();
//builder.Services.AddScoped<IRepository<User>, UserRepository>();
//builder.Services.AddAutoMapper(typeof(MyMapper));
builder.Services.AddDbContext<IContext, Database>();


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
