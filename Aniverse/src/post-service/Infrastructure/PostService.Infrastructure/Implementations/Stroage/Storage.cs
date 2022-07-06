using PostService.Application.DTO_s.Upload;
using ProjectCommon.ExtensionNameOperations;

namespace PostService.Infrastructure.Implementations.Stroage
{
    public class Storage
    {

        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected string FileRename(FileRenameDto fileRenamedto, HasFile hasFileMethod)
        {
            string extension = Path.GetExtension(fileRenamedto.File.FileName);
            string oldName = Path.GetFileNameWithoutExtension(fileRenamedto.File.FileName);
            string newFileName = $"{fileRenamedto.Username}" +
                                 $"{NameOperations.CharacterRegulatory(fileRenamedto.File.FileName)}" +
                                 $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}" +
                                 $"{extension}";
            if (hasFileMethod(fileRenamedto.ContainerName, newFileName))
                return FileRename(fileRenamedto, hasFileMethod);
            else
                return newFileName;
        }
    }
}
