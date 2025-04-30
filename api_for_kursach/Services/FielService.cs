using Microsoft.AspNetCore.Mvc;
using System.IO;

public class FileService
{
    private readonly string _baseFilePath; // Базовый путь к папке с файлами

    public FileService(string baseFilePath)
    {
        _baseFilePath = baseFilePath; // Например, "C:\\Files\\Tracks"
    }

    /// <summary>
    /// Получает файл по его относительному пути.
    /// </summary>
    /// <param name="relativePath">Относительный путь к файлу.</param>
    /// <returns>FileStream или null, если файл не найден.</returns>
    public FileStream GetFileStream(string relativePath)
    {
        var fullPath = Path.Combine(_baseFilePath, relativePath);

        if (!File.Exists(fullPath))
        {
            return null; // Файл не найден
        }

        return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
    }
}