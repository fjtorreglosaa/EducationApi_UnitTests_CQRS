using AutoMapper;
using Education.Application.Cursos;
using Education.Application.NUnitTests.Helper;
using Education.Domain;
using Education.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Education.Application.NUnitTests.Cursos
{
    [TestFixture]
    public class GetCursoByIdQueryNUnitTests
    {
        private GetCursoByIdQuery.GetCursoByIdQueryHandler handler;
        private Guid cursoIdTest;

        [SetUp]
        public void Setup()
        {
            cursoIdTest = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

            var testData = new List<Curso>
            {
                new Curso
                {
                    CursoId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709"),
                    Precio = 15,
                    Descripcion = "Test 1",
                    Titulo = "Test 1"
                },

                new Curso
                {
                    CursoId = Guid.NewGuid(),
                    Precio = 10,
                    Descripcion = "Test 2",
                    Titulo = "Test 2"
                }
            };

            //Base de datos de prueba
            var options = new DbContextOptionsBuilder<EducationDbContext>()
                .UseInMemoryDatabase(databaseName: "EducationDb")
                .Options;

            //Mock del contexto
            var educationDbContextMock = new EducationDbContext(options);

            //Se le agrega la data y las entidades al context
            educationDbContextMock.Cursos.AddRange(testData);
            var commit = educationDbContextMock.SaveChangesAsync();

            //Aqui se emula al mapper profile
            var mapConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            handler = new GetCursoByIdQuery.GetCursoByIdQueryHandler(educationDbContextMock, mapper);
        }

        [Test]
        public async Task GetCursoByIdQueryHandler_InputCursoId_ReturnsNotNull()
        {
            var request = new GetCursoByIdQuery.GetCursoByIdQueryRequest { Id = cursoIdTest };

            var result = await handler.Handle(request, new CancellationToken());

            Assert.IsNotNull(result);
        }
    }
}
