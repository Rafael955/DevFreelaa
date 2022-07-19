using Dapper;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;

        public StartProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            //Dapper
            var projectToStart = _dbContext.Projects.SingleOrDefault(x => x.Id == request.Id);

            projectToStart.Start();

            using (var sqlConn = new SqlConnection())
            {
                sqlConn.Open();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";

                await sqlConn.ExecuteAsync(script, new { status = projectToStart.Status, startedAt = projectToStart.StartedAt, request.Id });
                await _dbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
