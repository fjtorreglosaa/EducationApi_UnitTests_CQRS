using AutoMapper;
using Education.Application.DTO;
using Education.Domain;
using Education.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.Cursos
{
    public class GetCursoByIdQuery
    {
        public class GetCursoByIdQueryRequest : IRequest<CursoDto>
        {
            public Guid Id;
        }

        public class GetCursoByIdQueryHandler : IRequestHandler<GetCursoByIdQueryRequest, CursoDto>
        {
            private readonly EducationDbContext _educationDbContext;
            private readonly IMapper _mapper;

            public GetCursoByIdQueryHandler(EducationDbContext educationDbContext, IMapper mapper)
            {
                _educationDbContext = educationDbContext;
                _mapper = mapper;
            }

            public async Task<CursoDto> Handle(GetCursoByIdQueryRequest request, CancellationToken cancellationToken)
            {
                var cursos = await _educationDbContext.Cursos.ToListAsync();

                var curso = cursos.FirstOrDefault(x => x.CursoId == request.Id);

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);

                return cursoDto;
            }
        }
    }
}
