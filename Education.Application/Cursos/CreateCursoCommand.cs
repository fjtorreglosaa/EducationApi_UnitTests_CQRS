using Education.Domain;
using Education.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    public class CreateCursoCommand
    {
        public class CreateCursoCommandRequest : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public decimal Precio { get; set; }
        }

        public class CreateCursoCommandRequestValidation : AbstractValidator<CreateCursoCommandRequest>
        {
            public CreateCursoCommandRequestValidation()
            {
                RuleFor(c => c.Descripcion);
                RuleFor(c => c.Titulo);
            }
        }

        public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommandRequest>
        {
            private readonly EducationDbContext _educationDbContext;

            public CreateCursoCommandHandler(EducationDbContext educationDbContext)
            {
                _educationDbContext = educationDbContext;
            }

            public async Task<Unit> Handle(CreateCursoCommandRequest request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    CursoId = Guid.NewGuid(),
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow,
                    Precio = request.Precio
                };

                _educationDbContext.Add(curso);

                var commit = await _educationDbContext.SaveChangesAsync();

                return commit > 0 ? Unit.Value : throw new Exception("No se pudo insertar el curso.");
            }
        }
    }
}
