using AutoMapper;
using Education.Application.DTO;
using Education.Domain;
using Education.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.Cursos
{
    public class GetCursoQuery
    {
        public class GetCursoQueryRequest : IRequest<List<CursoDto>> { }

        public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, List<CursoDto>>
        {
            private readonly EducationDbContext _educationDbContext;
            private readonly IMapper _mapper;

            public GetCursoQueryHandler(EducationDbContext educationDbContext, IMapper mapper)
            {
                _educationDbContext = educationDbContext;
                _mapper = mapper;
            }

            public async Task<List<CursoDto>> Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
            {
                var cursos = await _educationDbContext.Cursos.ToListAsync();

                var cursosDto = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);

                return cursosDto;
            }
        }
    }
}
