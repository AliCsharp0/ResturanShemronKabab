using FrameWork.DTOS;

namespace ResturanShemronKabab.Framwork.UI.Services
{
    public interface IFileManager
    {
        bool RemoveFilter(string path);

        string ToPhysicalAddress(string FileName, string FolderName);

        OperationResult SaveFile(IFormFile file,  string FolderName);

        OperationResult ValidateFileSize(IFormFile file , long MinCapacity , long  MaxCapacity);

        bool ValidateFileName(string fileName);

		string ToUniquieFileName(string fileName);

		string ToRelativeAddress(string UniqeFileName , string Folder);
	}
}
