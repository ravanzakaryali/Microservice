namespace PostService.Application.Exceptions.CommonExceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message){ }
    }
}
