using PostService.Domain.Interfaces;

namespace PostService.Domain
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }
        Task SaveAsync();
    }
}
