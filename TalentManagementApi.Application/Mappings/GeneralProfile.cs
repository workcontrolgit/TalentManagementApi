using AutoMapper;
using TalentManagementApi.Application.Features.Employees.Queries.GetEmployees;
using TalentManagementApi.Application.Features.Positions.Commands.CreatePosition;
using TalentManagementApi.Application.Features.Positions.Queries.GetPositions;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();
        }
    }
}