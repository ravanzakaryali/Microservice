namespace PostService.Application.Exceptions.CommonExceptions
{
    public class AlreadyExistExceprion : ApplicationException
    {
        public AlreadyExistExceprion(string message) : base(message) { }
    }
}
