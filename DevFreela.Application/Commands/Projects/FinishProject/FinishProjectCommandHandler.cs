using Dapper;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {
        private readonly DevFreelaDbContext _dbContext;

        public FinishProjectCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToFinish = _dbContext.Projects.SingleOrDefault(x => x.Id == request.Id);

            projectToFinish.Finish();

            using (var sqlConn = new SqlConnection())
            {
                sqlConn.Open();

                var script = "UPDATE Projects SET Status = @status, FinishedAt = @finishedAt WHERE Id = @id";

                await sqlConn.ExecuteAsync(script, new { status = projectToFinish.Status, finishedAt = projectToFinish.FinishedAt, request.Id });
                await _dbContext.SaveChangesAsync();
            }


            return Unit.Value;
        }
    }
}
