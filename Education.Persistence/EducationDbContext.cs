using Education.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Education.Persistence
{
    public class EducationDbContext : DbContext
    {
        public EducationDbContext(DbContextOptions<EducationDbContext> options) : base(options)
        {

        }

        public DbSet<Curso> Cursos {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Curso>()
                .Property(x => x.Precio)
                .HasPrecision(14, 2);

            builder.Entity<Curso>().HasData(new Curso
            {
                CursoId = Guid.NewGuid(),
                Descripcion = "Curso de C# basico",
                Titulo = "C# Desde Cero",
                FechaCreacion = DateTime.Now,
                FechaPublicacion = DateTime.Now.AddDays(365),
                Precio = 56000
            });

            builder.Entity<Curso>().HasData(new Curso
            {
                CursoId = Guid.NewGuid(),
                Descripcion = "Curso de Java",
                Titulo = "Master en Java Spring desde las raices",
                FechaCreacion = DateTime.Now,
                FechaPublicacion = DateTime.Now.AddDays(365),
                Precio = 25000
            });

            builder.Entity<Curso>().HasData(new Curso
            {
                CursoId = Guid.NewGuid(),
                Descripcion = "Curso de Unit Tests para .NET Core",
                Titulo = "Master en Unit Tests con CQRS",
                FechaCreacion = DateTime.Now,
                FechaPublicacion = DateTime.Now.AddDays(365),
                Precio = 75000
            });

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
