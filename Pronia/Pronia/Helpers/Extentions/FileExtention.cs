namespace Pronia.Helpers.Extentions;

public static class FileExtention
{
    public static string CreateFile(this IFormFile file, string webRootPath, string folderName)
    {
        string fileName = Guid.NewGuid().ToString() + file.FileName;
        string path = Path.Combine(webRootPath, folderName, fileName);
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return fileName;
    }

    public static void RemoveFile(string webRoot, string folderName, string fileName)
    {
        string path = Path.Combine(webRoot, folderName, fileName);
        if (File.Exists(path)) File.Delete(path);
    }
}
