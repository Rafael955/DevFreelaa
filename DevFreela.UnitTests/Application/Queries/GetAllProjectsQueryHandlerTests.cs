using DevFreela.Application.Queries;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectsViewModel()
        {
            //Arrange
            var projects = new List<Project>
            {
                new Project("Nome Do Teste 1", "Descricao De Teste 1",4,5,20000),
                new Project("Nome Do Teste 2", "Descricao De Teste 2",4,5,30000),
                new Project("Nome Do Teste 3", "Descricao De Teste 3",4,5,10000)
            };

            var query = string.Empty;

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(x => x.GetAllAsync(query).Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            //Act
            var projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            //Assert
            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            //Verifica se o método GetAllAsync foi executado pelo menos UMA vez!!!
            projectRepositoryMock.Verify(x => x.GetAllAsync(query).Result, Times.Once);
        }
    }
}
