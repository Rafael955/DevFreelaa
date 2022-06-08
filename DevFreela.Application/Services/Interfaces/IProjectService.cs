using DevFreela.Application.InputModels;
using DevFreela.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        List<ProjectViewModel> GetAll(string query);

        ProjectDetailsViewModel GetById(int id);

        int Create(NewProjectInputModel inputModel);

        void Update(UpdateProjectInputModel inputModel);

        void Delete(int id);

        void CreateComment(CreateCommentInputModel inputModel);

        void Start(int id);

        void Finish(int id);
    }

    // Obs¹: Evitar reutilizar o mesmo InputModel para outras funcionalidades. Ex: Não reutilizar o NewProjectViewModel do Create no método de Update pois é possível ter campos em NewProjectInputModel que não poderão ser atualizados
}
