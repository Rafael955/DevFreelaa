using DevFreela.Application.Commands.Projects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
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
