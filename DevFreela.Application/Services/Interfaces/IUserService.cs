using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using System.Collections.Generic;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetAll(string query);

        UserDetailsViewModel GetById(int id);

        int Create(NewUserInputModel inputModel);

        void Update(UpdateUserInputModel inputModel);

        void Delete(int id);
    }
}
