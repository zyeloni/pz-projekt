using System.Threading.Tasks;

namespace API.Services
{
    public interface ITemplateService
    {
        Task<string> RenderAsync<TViewModel>(string templateFileName, TViewModel viewModel);
    }
}