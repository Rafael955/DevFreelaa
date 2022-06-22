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
            _dbContext.SaveChanges();

            return user.Id;
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
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

            if (user != null)
            {
                var userViewModel = new UserDetailsViewModel(user.Fullname, user.Email, user.BirthDate, user.CreatedAt, user.Active);
                return userViewModel;
            }

            return null;
        }

        public void Update(UpdateUserInputModel inputModel)
        {
            var userToUpdate = _dbContext.Users.FirstOrDefault(x => x.Id == inputModel.Id);

            if (userToUpdate != null)
            {
                userToUpdate.Update(inputModel.Fullname, inputModel.Email, inputModel.BirthDate);
                
                _dbContext.Users.Update(userToUpdate);
                _dbContext.SaveChanges();
            }
        }
    }
}
