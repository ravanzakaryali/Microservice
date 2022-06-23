namespace PostService.Application.Exceptions.FileExceptions
{
    public class FileTypeException : FileException
    {
        public FileTypeException(string message) :base(message){ }
    }
}
