using System.Collections.Generic;
using System.Threading.Tasks;
using TalentManagementApi.Application.Features.Employees.Queries.GetEmployees;
using TalentManagementApi.Application.Parameters;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged employee response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface IEmployeeRepositoryAsync : IGenericRepositoryAsync<Employee>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetEmployeeResponseAsync(GetEmployeesQuery requestParameters);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEmployeeResponseAsync(PagedEmployeesQuery requestParameters);
    }
}