namespace HR_MVC_ITI.Services;

public interface IFileSystem
{
    void CreateDirectory(string path);
    Stream CreateFile(string path);
    string Combine(params string[] paths);
    string GetExtension(string path);
}

public class PhysicalFileSystem : IFileSystem
{
    public void CreateDirectory(string path) => Directory.CreateDirectory(path);
    public Stream CreateFile(string path) => new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
    public string Combine(params string[] paths) => Path.Combine(paths);
    public string GetExtension(string path) => Path.GetExtension(path);
}
