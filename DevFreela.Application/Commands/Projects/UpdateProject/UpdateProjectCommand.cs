using MediatR;

namespace DevFreela.Application.Commands.Project
{ 
    public class UpdateProjectCommand : IRequest<Unit>
    {
        public UpdateProjectCommand(int id, string title, string description, decimal totalCost)
        {
            Id = id;
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public decimal TotalCost { get; private set; }
    }
}
