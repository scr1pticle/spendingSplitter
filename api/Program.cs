using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<SplitterContext>(opt =>
    opt.UseInMemoryDatabase("Splitter"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();