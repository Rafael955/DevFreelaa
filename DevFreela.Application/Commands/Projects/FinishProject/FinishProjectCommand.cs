using MediatR;
using System.Net;

namespace DevFreela.Application.Commands.Projects
{
    public class FinishProjectCommand : IRequest<HttpStatusCode>
    {
        public int Id { get; set; }

        public string CreditCardNumber { get; set; }

        public string Cvv { get; set; }

        public string ExpiresAt { get; set; }

        public string Fullname { get; set; }
    }
}
