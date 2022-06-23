using PostService.Application.Services.Interfaces;

namespace PostService.Application.Services
{
    public interface IUnitOfWorkService
    {
        IPostService PostService { get; }
    }
}
