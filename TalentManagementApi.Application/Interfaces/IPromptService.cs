using System.Threading.Tasks;

namespace TalentManagementApi.Application.Interfaces
{
    public interface IPromptService
    {
        Task<string> TriggerOpenAI(string prompt);
    }
}