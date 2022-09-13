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
    public class FinishProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputIdIsValid_TryingToFinishAProject_ProjectIsSuccessfullyFinished()
        {
            // Arrange
            var projectToFinishMock = new List<Project>()
            {
                new Project("Projeto de Teste","Projeto a ser encerrado",1,3, 50000)
            };

            var query = "";
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.GetAllAsync(query).Result).Returns(projectToFinishMock);

            var finishProjectCommand = new FinishProjectCommand(0);

            var finishProjectCommandHandler = new FinishProjectCommandHandler(projectRepositoryMock.Object);

            // Act
            await finishProjectCommandHandler.Handle(finishProjectCommand, new CancellationToken());

            // Assert
            projectRepositoryMock.Verify(x => x.FinishProjectAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
