namespace MyMarket.Services.FileSystem.Contracts
{
    using System.Web;

    public interface IFileSystemService
    {
        string SaveImage(HttpPostedFileBase image, string path);
    }
}