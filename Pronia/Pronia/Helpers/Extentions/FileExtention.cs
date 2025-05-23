namespace Pronia.Helpers.Extentions;

public static class FileExtention
{
    public static string CreateFile(this IFormFile file, string webRootPath, string folderName)
    {
        if (!IsValidFile(file)) return String.Empty;

        string fileName = Guid.NewGuid().ToString() + file.FileName;
        string path = Path.Combine(webRootPath, folderName, fileName);
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return fileName;
    }

    public static string UpdateFile(this IFormFile file, string webRootPath, string folderName, string oldUrl)
    {
        if (!IsValidFile(file)) return String.Empty;

        RemoveFile(Path.Combine(webRootPath, folderName, oldUrl));
        return file.CreateFile(webRootPath, folderName);
    }

    public static void RemoveFile(string path)
    {
        if (File.Exists(path)) System.IO.File.Delete(path);
    }

    public static bool IsValidFile(IFormFile file)
    {
        if (file is null) return false;
        if (!file.ContentType.Contains("image")) return false;
        if (file.Length > 2097152 && file.Length == 0) return false;

        return true;
    }
}
