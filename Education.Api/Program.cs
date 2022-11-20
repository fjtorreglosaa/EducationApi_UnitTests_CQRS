using Education.Application.Cursos;
using Education.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("educationDB"),
        b => b.MigrationsAssembly("Education.Persistence")));

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateCursoCommand>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetCursoQuery.GetCursoQueryHandler).Assembly);
builder.Services.AddAutoMapper(typeof(GetCursoQuery.GetCursoQueryHandler));

builder.Services.AddCors(x => x.AddPolicy("corsapp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("corsapp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
