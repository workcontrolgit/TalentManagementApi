using MediatR;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace TalentManagementApi.Application.Features.Positions.Commands.CreatePosition
{
    public partial class InsertMockPositionCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedPositionCommandHandler : IRequestHandler<InsertMockPositionCommand, Response<int>>
    {
        private readonly IPositionRepositoryAsync _repository;

        public SeedPositionCommandHandler(IPositionRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(InsertMockPositionCommand request, CancellationToken cancellationToken)
        {
            await _repository.SeedDataAsync(request.RowCount);
            return new Response<int>(request.RowCount);
        }
    }
}