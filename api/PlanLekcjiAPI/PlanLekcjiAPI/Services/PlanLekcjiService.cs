using PlanLekcjiAPI.Entities;
using System.IO.Compression;

namespace PlanLekcjiAPI.Services;

public interface IPlanLekcjiService
{
    Task<string> SaveAndUnZipFile(IFormFile file, Func<string, Task> afterExtractionCallback = null);
}

public class PlanLekcjiService : IPlanLekcjiService
{
    private readonly string _uploadFolderPath;

    public PlanLekcjiService()
    {
        _uploadFolderPath = Path.Combine(@"./../../../", "data-plan-lekcji");
    }

    public async Task<string> SaveAndUnZipFile(IFormFile file,
        Func<string, Task> afterExtractionCallback = null)
    {
        // Tworzenie folderu na przesłane pliki, jeśli nie istnieje
        if (!Directory.Exists(_uploadFolderPath))
        {
            Directory.CreateDirectory(_uploadFolderPath);
        }

        ClearDirectory(_uploadFolderPath);

        // Ścieżka do zapisu pliku ZIP
        var filePath = Path.Combine(_uploadFolderPath, file.FileName);

        // Zapis pliku na serwer
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Ścieżka do wypakowania
        var extractPath = Path.Combine(_uploadFolderPath, Path.GetFileNameWithoutExtension(file.FileName));


        // Wypakowywanie pliku ZIP
        try
        {
            ZipFile.ExtractToDirectory(filePath, extractPath, overwriteFiles: true);

            // Wywołanie metody callback, jeśli została przekazana
            if (afterExtractionCallback != null)
            {
                await afterExtractionCallback(extractPath);
            }

            //ClearDirectory(_uploadFolderPath);
        }
        catch (InvalidDataException)
        {
            throw new ArgumentException("The file is not a valid ZIP archive.");
        }
        catch (IOException ex)
        {
            throw new IOException($"Wystąpił błąd podczas wypakowywania pliku: {ex.Message}");
        }

        return extractPath;
    }

    private void ClearDirectory(string path)
    {
        if (Directory.GetFiles(path).Length == 0)
            return;

        // Usunięcie wszystkich plików i folderów w katalogu
        foreach (var file in Directory.GetFiles(path))
        {
            File.Delete(file);
        }

        foreach (var directory in Directory.GetDirectories(path))
        {
            Directory.Delete(directory, recursive: true);
        }
    }



}