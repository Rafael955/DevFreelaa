using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewUserInputModel inputModel)
        {
            var user = new User(inputModel.Fullname, inputModel.Email, inputModel.BirthDate);
            _dbContext.Users.Add(user);

            return user.Id;
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if(user != null)
                _dbContext.Users.Remove(user);
        }

        public List<UserViewModel> GetAll(string query)
        {
            var users = _dbContext.Users;

            var usersViewModel = users.Select(x => new UserViewModel(x.Fullname, x.Email, x.CreatedAt)).ToList();

            return usersViewModel;
        }

        public UserDetailsViewModel GetById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if(user != null)
            {
                var userViewModel = new UserDetailsViewModel(user.Fullname, user.Email, user.BirthDate, user.CreatedAt, user.Active);
                return userViewModel;
            }

            return null;
        }

        public void Update(UpdateUserInputModel inputModel)
        {
            var userToUpdate = _dbContext.Users.FirstOrDefault(x => x.Id == inputModel.Id);

            //Simulando Update em uma base de dados
            userToUpdate.Update(inputModel.Fullname, inputModel.Email, inputModel.BirthDate);
        }
    }

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
