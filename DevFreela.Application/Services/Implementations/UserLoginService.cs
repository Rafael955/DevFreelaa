using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserLoginService : IUserLoginService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserLoginService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserLoginViewModel GetByUsername(UserLoginInputModel user)
        {
            var userLogin = _dbContext.UsersLogin.FirstOrDefault(x => x.Username == user.Username);

            UserLoginViewModel userLoginViewModel = null;

            if (userLogin != null)
                userLoginViewModel = new UserLoginViewModel(userLogin.Username, userLogin.Password, userLogin.Email, userLogin.isLogged);

            return userLoginViewModel;
        }

        public bool Login(UserLoginInputModel inputModel)
        {
            var userLogin = _dbContext.UsersLogin.FirstOrDefault(x => x.Username == inputModel.Username && inputModel.Password == inputModel.Password);

            if (userLogin == null)
                return false;

            if (userLogin.isLogged)
                return false;

            userLogin.Login();

            return true;
        }

        public bool Logoff(UserLoginInputModel inputModel)
        {
            var userLogin = _dbContext.UsersLogin.FirstOrDefault(x => x.Username == inputModel.Username && inputModel.Password == inputModel.Password);

            if (userLogin == null)
                return false;

            if (userLogin.isLogged)
                return false;

            userLogin.Logoff();

            return true;
        }
    }
}
