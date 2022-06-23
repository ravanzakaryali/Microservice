namespace PostService.Application.Exceptions.FileExceptions
{
    public class FileSizeException : FileException
    {
        public FileSizeException(string message) : base(message) { }
    }
}
