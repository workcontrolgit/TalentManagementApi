using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TalentManagementApi.Application.Interfaces;
using TalentManagementApi.Application.Interfaces.Repositories;
using TalentManagementApi.Application.Parameters;

namespace TalentManagementApi.Application.Features.Departments.Queries.GetDepartments
{
    /// <summary>
    /// GetAllDepartmentsQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetDepartmentsQuery : ShapeParameter, IRequest<IEnumerable<GetDepartmentsViewModel>>
    {
    }

    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, IEnumerable<GetDepartmentsViewModel>>
    {
        private readonly IDepartmentRepositoryAsync _repository;
        private readonly IModelHelper _modelHelper;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for GetAllDepartmentsQueryHandler class.
        /// </summary>
        /// <param name="repository">IDepartmentRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllDepartmentsQueryHandler object.
        /// </returns>
        public GetAllDepartmentsQueryHandler(IDepartmentRepositoryAsync repository, IModelHelper modelHelper, IMapper mapper)
        {
            _repository = repository;
            _modelHelper = modelHelper;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetDepartmentsQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetDepartmentsQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<IEnumerable<GetDepartmentsViewModel>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var fields = request.Fields;
            var orderBy = request.OrderBy;

            //filtered fields security
            if (!string.IsNullOrEmpty(request.Fields))
            {
                //limit to fields in view model
                fields = _modelHelper.ValidateModelFields<GetDepartmentsViewModel>(request.Fields);
            }
            else
            {
                //default fields from view model
                fields = _modelHelper.GetModelFields<GetDepartmentsViewModel>();
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                //limit to fields in view model
                orderBy = _modelHelper.ValidateModelFields<GetDepartmentsViewModel>(orderBy);
            }
            else
            {
                //default fields from view model
                orderBy = "Name";
            }

            // var data = await _repository.GetAllAsync();
            var data = await _repository.GetAllShapeAsync(orderBy, fields);

            var dtos = _mapper.Map<IEnumerable<GetDepartmentsViewModel>>(data);

            return dtos;
        }
    }
}