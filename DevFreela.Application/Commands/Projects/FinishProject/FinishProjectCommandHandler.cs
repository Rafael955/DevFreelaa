using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Projects
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {
        private readonly IProjectRepository _repository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository repository, IPaymentService paymentService)
        {
            _repository = repository;
            _paymentService = paymentService;
        }

        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToFinish = (await _repository.GetAllAsync()).SingleOrDefault(x => x.Id == request.Id);

            projectToFinish.Finish();

            // Processar Pagamentos - Utilização de microsserviço(API) para pagamentos.

            var paymentInfoDto = new PaymentInfoDTO(request.Id, request.CreditCardNumber, request.Cvv, request.ExpiresAt, request.Fullname);

            var result = await _paymentService.ProcessPayment(paymentInfoDto);

            if (!result)
                projectToFinish.SetPaymentPending();

            await _repository.FinishProjectAsync(projectToFinish);

            return Unit.Value;
        }
    }
}
