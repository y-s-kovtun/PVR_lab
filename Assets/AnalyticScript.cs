using UnityEngine;
using TMPro;
using UnityEngine.Profiling;

public class AnalyticScript : MonoBehaviour
{
    public TextMeshProUGUI textElement; // UI Text элемент для вывода данных
    private float startTime;

    void Start()
    {
        // Сохраняем время запуска
        startTime = Time.realtimeSinceStartup;

        if (textElement == null)
        {
            Debug.LogError("Text Element is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (textElement == null) return;

        // Расчёт FPS
        float fps = 1.0f / Time.deltaTime;

        // Время загрузки
        float loadTime = Time.realtimeSinceStartup - startTime;

        // Использование памяти (в байтах, килобайтах и мегабайтах)
        float memoryUsageBytes = Profiler.GetTotalAllocatedMemoryLong();
        string memoryUsage = FormatMemory(memoryUsageBytes);

        // Процент использования CPU (примерная оценка)
        float cpuUsagePercent = (Profiler.GetTotalReservedMemoryLong() / (float)Profiler.GetTotalAllocatedMemoryLong()) * 100f;

        // Обновление текста
        textElement.text = $"FPS: {fps:F1}\n" +
                           $"Memory: {memoryUsage}\n" +
                           $"CPU Usage: {cpuUsagePercent:F1}%\n" +
                           $"Load Time: {loadTime:F1} s\n" +
                           $"Delta Time: {Time.deltaTime:F4} s";
    }

    // Форматирует использование памяти в удобочитаемую строку
    private string FormatMemory(float memoryBytes)
    {
        if (memoryBytes < 1024)
        {
            return $"{memoryBytes:F1} B";
        }
        else if (memoryBytes < 1024 * 1024)
        {
            return $"{memoryBytes / 1024:F1} KB";
        }
        else
        {
            return $"{memoryBytes / (1024 * 1024):F1} MB";
        }
    }
}
