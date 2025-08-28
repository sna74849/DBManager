using System;
using System.IO;
using System.Text;

/// <summary>
/// Provides functionality to save exception information (Message, StackTrace)
/// into log files. Each log file is created per day.
/// </summary>
public class ExceptionLogger
{
    /// <summary>
    /// Path to the directory where log files will be stored.
    /// </summary>
    private readonly string logDirectory;

    /// <summary>
    /// Prefix for the log file name (e.g., "error" → "error_2025-08-10.log").
    /// </summary>
    private readonly string logFilePrefix;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionLogger"/> class.
    /// </summary>
    /// <param name="logDirectory">The directory path to store log files.</param>
    /// <param name="logFilePrefix">The prefix for the log file name. Default is "db-error".</param>
    public ExceptionLogger(string logDirectory, string logFilePrefix = "db-error")
    {
        this.logDirectory = logDirectory;
        this.logFilePrefix = logFilePrefix;

        // Create directory if it does not exist
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
    }

    /// <summary>
    /// Saves the specified exception into the daily log file.
    /// New logs are prepended, so the latest log entry appears at the top of the file.
    /// </summary>
    /// <param name="ex">The exception to be logged.</param>
    public void LogException(Exception ex)
    {
        if (ex == null) return;

        // Daily log file name
        string logFileName = $"{logFilePrefix}_{DateTime.Now:yyyy-MM-dd}.log";
        string logFilePath = Path.Combine(logDirectory, logFileName);

        // Build log entry
        var sb = new StringBuilder();
        sb.AppendLine("========================================");
        sb.AppendLine($"[DateTime] {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"[Message]  {ex.Message}");
        sb.AppendLine($"[StackTrace]");
        sb.AppendLine(ex.StackTrace ?? "No stack trace available");
        sb.AppendLine("========================================");
        sb.AppendLine();

        string newEntry = sb.ToString();

        // Prepend new entry so the latest log appears first
        if (File.Exists(logFilePath))
        {
            string existingContent = File.ReadAllText(logFilePath, Encoding.UTF8);
            File.WriteAllText(logFilePath, newEntry + existingContent, Encoding.UTF8);
        }
        else
        {
            File.WriteAllText(logFilePath, newEntry, Encoding.UTF8);
        }
    }
}
