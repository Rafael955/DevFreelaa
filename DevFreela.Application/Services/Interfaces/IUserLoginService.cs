using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IUserLoginService
    {
        UserLoginViewModel GetByUsername(UserLoginInputModel user);
        bool Login(UserLoginInputModel inputModel);
        bool Logoff(UserLoginInputModel inputModel);
    }
}
