namespace PostService.Application.Exceptions.CommonExceptions
{
    public class NullFoundException : ApplicationException
    {
        public NullFoundException(string message) : base(message){ }
    }
}
