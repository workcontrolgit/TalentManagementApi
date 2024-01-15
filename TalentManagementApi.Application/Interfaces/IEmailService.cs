using TalentManagementApi.Application.DTOs.Email;
using System.Threading.Tasks;

namespace TalentManagementApi.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}