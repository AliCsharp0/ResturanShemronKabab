using FrameWork.DTOS;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ResturanShemronKabab.Framwork.UI.Services;
using System.Net.NetworkInformation;

namespace ResturanShemronKabab.Frawwork.UI
{
	public class FileManager : IFileManager
	{
		private readonly IHostEnvironment env;

		public FileManager(IHostEnvironment env)
		{
			this.env = env;
		}

		public bool RemoveFilter(string path)
		{
			if (path == null || path.ToLower() == "~/images/noimage.png")
			{
				return false;
			}
			if (!System.IO.Directory.Exists(path))
			{
				return false;
			}
			System.IO.File.Delete(path);
			return true;
		}

		public OperationResult SaveFile(IFormFile file, string FolderName)
		{
			OperationResult op = new OperationResult();
			var address = Path.GetFileName(file.FileName);
			string uniqeFile = ToUniquieFileName(address);
			address = ToPhysicalAddress(uniqeFile, FolderName);
			FileStream fs = new FileStream(address, FileMode.Create);
			try
			{
				file.CopyTo(fs);
				return op.ToSuccess(uniqeFile);
			}
			catch (Exception ex)
			{
				return op.ToFail(ex.Message);
			}
			finally
			{
				fs.Close();
				fs.Dispose();
			}
		}

		public string ToPhysicalAddress(string FileName, string FolderName)
		{
			return env.ContentRootPath + @"\wwwroot\" + FolderName + @"\" + FileName;
		}

		public string ToRelativeAddress(string UniqeFileName, string Folder)
		{
			return @"~/" + Folder + @"/" + UniqeFileName;
		}

		public string ToUniquieFileName(string fileName)
		{
			return Guid.NewGuid().ToString().Replace("-", "_") + fileName;
		}

		public bool ValidateFileName(string fileName)
		{
			if (fileName == null)
			{
				return false;
			}
			fileName = fileName.Trim().ToLower();
			if (fileName.Contains(".php") || fileName.Contains(".asp"))
			{
				return false;
			}
			return true;
		}

		public OperationResult ValidateFileSize(IFormFile file, long MinCapacity, long MaxCapacity)
		{
			OperationResult op = new OperationResult();
			if (file.Length < MinCapacity || file.Length > MaxCapacity)
			{
				return op.ToFail("Invalid File Size ");
			}
			else
			{
				return op.ToSuccess("File Size is Valid");
			}
		}
	}
}
