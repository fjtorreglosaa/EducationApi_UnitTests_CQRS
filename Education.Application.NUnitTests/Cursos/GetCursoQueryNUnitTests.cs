using AutoFixture;
using AutoMapper;
using Education.Application.Cursos;
using Education.Application.NUnitTests.Helper;
using Education.Domain;
using Education.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Application.NUnitTests.Cursos
{
    [TestFixture]
    public class GetCursoQueryNUnitTests
    {
        private GetCursoQuery.GetCursoQueryHandler handler;

        [SetUp]
        public void Setup()
        {
            // Aqui se emula al context
            var fixture = new Fixture();
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            cursoRecords.Add(fixture.Build<Curso>()
                .With(x => x.CursoId, Guid.Empty)
                .Create());

            //Base de datos de prueba: siempre se genera una nueva aleatoria
            var options = new DbContextOptionsBuilder<EducationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EducationDbContext-{Guid.NewGuid()}")
                .Options;

            //Mock del contexto
            var educationDbContextMock = new EducationDbContext(options);

            //Se le agrega la data y las entidades al contexto
            educationDbContextMock.Cursos.AddRange(cursoRecords);

            //Aqui se emula al mapper profile

            var mapConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            // Aqui se instancia un objeto de la clase GetCursoQuery.GetCursoQueryHandler y pasarle como parametros los objetos context y mapping

            handler = new GetCursoQuery.GetCursoQueryHandler(educationDbContextMock, mapper);
        }

        [Test]
        public async Task GetCursoQueryHandler_ConsultaDeCursos_ReturnsTrue()
        {
            var request = new GetCursoQuery.GetCursoQueryRequest();

            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.IsNotNull(result);
        }
    }
}
