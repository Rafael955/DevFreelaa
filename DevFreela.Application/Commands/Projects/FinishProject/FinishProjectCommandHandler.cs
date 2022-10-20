using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using DevFreela.Application.Queries;
using System.Net;

namespace DevFreela.Application.Commands.Projects
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, HttpStatusCode>
    {
        private readonly IProjectRepository _repository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository repository, IPaymentService paymentService)
        {
            _repository = repository;
            _paymentService = paymentService;
        }

        public async Task<HttpStatusCode> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToFinish = await _repository.GetProjectByIdAsync(request.Id);

            if (projectToFinish.Status == ProjectStatusEnum.Created)
                return HttpStatusCode.BadRequest;

            // Processar Pagamentos - Utilização de microsserviço(API) para pagamentos.

            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.Fullname);

            // Coloca projeto na fila mensageria para processar o pagamento do mesmo.
            _paymentService.ProcessPayment(paymentInfoDto);

            projectToFinish.SetPaymentPending();

            await _repository.FinishProjectAsync(projectToFinish);

            return HttpStatusCode.Accepted;
        }
    }
}
