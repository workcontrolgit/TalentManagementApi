using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for Customer entity with asynchronous methods.
    /// </summary>
    /// <param name="positionNumber">Customer number to check for uniqueness.</param>
    /// <returns>
    /// Task indicating whether the position number is unique.
    /// </returns>
    /// <param name="rowCount">Number of rows to seed.</param>
    /// <returns>
    /// Task indicating the completion of seeding.
    /// </returns>
    /// <param name="requestParameters">Parameters for the query.</param>
    /// <param name="data">Data to be returned.</param>
    /// <param name="recordsCount">Number of records.</param>
    /// <returns>
    /// Task containing the paged response.
    /// </returns>
    public interface IEmployeeSearchAsync : IGenericSearchAsync<Employee>
    {
    }
}