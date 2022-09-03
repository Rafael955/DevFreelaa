using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDetailsViewModel>
    {
        private readonly DevFreelaDbContext _dbContext;

        public GetUserByIdQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDetailsViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == request.Id);

            if (user != null)
            {
                var userViewModel = new UserDetailsViewModel(user.Fullname, user.Email, user.BirthDate, user.CreatedAt, user.Active);
                return userViewModel;
            }

            return null;
        }
    }
}
