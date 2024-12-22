using UnityEngine;
using TMPro;
using UnityEngine.Profiling;

public class AnalyticScript : MonoBehaviour
{
    public TextMeshProUGUI textElement; // UI Text ������� ��� ������ ������
    private float startTime;

    void Start()
    {
        // ��������� ����� �������
        startTime = Time.realtimeSinceStartup;

        if (textElement == null)
        {
            Debug.LogError("Text Element is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (textElement == null) return;

        // ������ FPS
        float fps = 1.0f / Time.deltaTime;

        // ����� ��������
        float loadTime = Time.realtimeSinceStartup - startTime;

        // ������������� ������ (� ������, ���������� � ����������)
        float memoryUsageBytes = Profiler.GetTotalAllocatedMemoryLong();
        string memoryUsage = FormatMemory(memoryUsageBytes);

        // ������� ������������� CPU (��������� ������)
        float cpuUsagePercent = (Profiler.GetTotalReservedMemoryLong() / (float)Profiler.GetTotalAllocatedMemoryLong()) * 100f;

        // ���������� ������
        textElement.text = $"FPS: {fps:F1}\n" +
                           $"Memory: {memoryUsage}\n" +
                           $"CPU Usage: {cpuUsagePercent:F1}%\n" +
                           $"Load Time: {loadTime:F1} s\n" +
                           $"Delta Time: {Time.deltaTime:F4} s";
    }

    // ����������� ������������� ������ � ������������� ������
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
