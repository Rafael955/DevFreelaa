using DevFreela.Application.Commands.Project;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executes_ReturnProjectId()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IProjectRepository>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Descricao do projeto teste",
                TotalCost = 40000,
                IdClient = 4,
                IdFreelancer = 5
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            // Assert
            Assert.True(id >= 0);

            projectRepositoryMock.Verify(x => x.CreateProjectAsync(It.IsAny<Project>()), Times.Once);
        }
    }

    public class DeleteProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputIdIsValid_TryingToDeleteProject_ProjectIsSuccessfullyDeleted()
        {
            // Arrange
            var projectToDeleteMock = new List<Project>
            {
                new Project("Nome Do Teste 1", "Descricao De Teste 1", 4, 5, 20000)
            };

            var query = "";
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.GetAllAsync(query).Result).Returns(projectToDeleteMock);

            var deleteProjectCommand = new DeleteProjectCommand(0);

            var deleteProjectCommandHandler = new DeleteProjectCommandHandler(projectRepositoryMock.Object);

            //Act
            await deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken());

            //Assert
            projectRepositoryMock.Verify(x => x.DeleteProjectAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
