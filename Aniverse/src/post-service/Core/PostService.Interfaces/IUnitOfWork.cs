using PostService.Inerfaces.Interfaces;

namespace PostService.Inerfaces
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }
        Task SaveAsync();
    }
}
