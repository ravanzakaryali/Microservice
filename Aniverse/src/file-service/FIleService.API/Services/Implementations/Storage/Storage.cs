using Aniverse.Shared.Extensions;
using FileService.API.DTO_s.Upload;

namespace FileService.API.Services.Implementations.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected string FileRename(FileRenameDto fileRenamedto,HasFile hasFileMethod)
        {
            string extension = Path.GetExtension(fileRenamedto.Filename);
            string oldName = Path.GetFileNameWithoutExtension(fileRenamedto.Filename);
            string newFileName = $"{fileRenamedto.Username}" +
                                 $"{NameOperations.CharacterRegulatory(fileRenamedto.Filename)}" +
                                 $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}" +
                                 $"{extension}";
            if (hasFileMethod(fileRenamedto.ContainerName, newFileName))
                return FileRename(fileRenamedto, hasFileMethod);
            else
                return newFileName;
        }
    }
}
