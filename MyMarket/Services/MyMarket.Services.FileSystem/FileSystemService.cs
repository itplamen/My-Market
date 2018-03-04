namespace MyMarket.Services.FileSystem
{
    using System;
    using System.IO;
    using System.Web;

    using Bytes2you.Validation;

    using Contracts;

    public class FileSystemService : IFileSystemService
    {
        public string SaveImage(HttpPostedFileBase image, string path)
        {
            Guard.WhenArgument(image, nameof(image)).IsNull().Throw();
            Guard.WhenArgument(path, nameof(path)).IsNullOrEmpty().Throw();
            Guard.WhenArgument(Directory.Exists(path), nameof(path)).IsFalse().Throw();

            return this.SaveFile(image, path.Trim());
        }

        private string SaveFile(HttpPostedFileBase file, string filePath)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = string.Format("{0}{1}", Guid.NewGuid(), fileExtension);
            string urlPath = Path.Combine(filePath, fileName);

            file.SaveAs(urlPath);

            return urlPath;
        }
    }
}