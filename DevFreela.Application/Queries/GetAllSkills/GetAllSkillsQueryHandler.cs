using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>>
    {
        private readonly ISkillRepository _repository;

        public GetAllSkillsQueryHandler(ISkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            List<SkillViewModel> listSkills = new List<SkillViewModel>();

            var skills = await _repository.GetAllAsync();

            foreach (var skill in skills)
            {
                var skillViewModel = new SkillViewModel(skill.Id, skill.Description);

                listSkills.Add(skillViewModel);
            }

            return listSkills;
        }
    }
}
