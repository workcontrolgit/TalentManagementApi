using MediatR;
using TalentManagementApi.Application.Interfaces;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Application.Parameters;
using TalentManagementApi.Application.Wrappers;
using TalentManagementApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TalentManagementApi.Application.Features.Employees.Queries.GetEmployees
{
    /// <summary>
    /// GetAllEmployeesQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetEmployeesQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        //examples:
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string EmployeeNumber { get; set; }
        public string PositionTitle { get; set; }
    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IModelHelper _modelHelper;

        /// <summary>
        /// Constructor for GetAllEmployeesQueryHandler class.
        /// </summary>
        /// <param name="employeeRepository">IEmployeeRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllEmployeesQueryHandler object.
        /// </returns>
        public GetAllEmployeesQueryHandler(IEmployeeRepositoryAsync employeeRepository, IModelHelper modelHelper)
        {
            _employeeRepository = employeeRepository;
            _modelHelper = modelHelper;
        }

        /// <summary>
        /// Handles the GetEmployeesQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetEmployeesQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var validatedRequest = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validatedRequest.Fields))
            {
                //limit to fields in view model
                validatedRequest.Fields = _modelHelper.ValidateModelFields<GetEmployeesViewModel>(validatedRequest.Fields);
            }
            if (string.IsNullOrEmpty(validatedRequest.Fields))
            {
                //default fields from view model
                validatedRequest.Fields = _modelHelper.GetModelFields<GetEmployeesViewModel>();
            }
            // query based on filter
            var qryResult = await _employeeRepository.GetPagedEmployeeResponseAsync(validatedRequest);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validatedRequest.PageNumber, validatedRequest.PageSize, recordCount);
        }
    }
}