
using System.Collections.Concurrent;
namespace Ultra_Saver.Configuration;

public class FileLogger : ILogger
{

    private readonly string _name;
    private readonly Func<FileLoggerConfiguration> _getCurrentConfig;

    public FileLogger(string name, Func<FileLoggerConfiguration> getCurrentConfig) =>
        (_getCurrentConfig, _name) = (getCurrentConfig, name);

    public IDisposable BeginScope<TState>(TState state) => default!;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _getCurrentConfig().Level;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }


        var fs = _getCurrentConfig().LogFile;

        lock (fs)
        {
            fs.WriteLine($"[{logLevel}] ({DateTime.Now}) {state} {exception?.Data}\n{exception?.StackTrace}");
            fs.Flush();
        }
    }
}

public sealed class FileLoggerConfiguration
{
    public FileLoggerConfiguration(string filename, LogLevel level = LogLevel.Information)
    {
        _file = new Lazy<StreamWriter>(new StreamWriter(filename));
        Level = level;
    }

    ~FileLoggerConfiguration()
    {
        _file.Value.Flush();
        _file.Value.Close();
    }

    public LogLevel Level { get; set; }

    public long MaxFileSize { get; set; } = (long)1e7; //10 Mb

    private Lazy<StreamWriter> _file;

    public StreamWriter LogFile
    {
        get => _file.Value;
    }

}

[ProviderAlias("File")]
public sealed class FileLoggerProvider : ILoggerProvider
{
    private readonly FileLoggerConfiguration _currentConfig;
    private readonly ConcurrentDictionary<string, FileLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public FileLoggerProvider(FileLoggerConfiguration config)
    {
        _currentConfig = config;
    }

    public FileLoggerConfiguration GetCurrentConfig() => _currentConfig;

    public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new FileLogger(name, GetCurrentConfig));

    public void Dispose()
    {
        _loggers.Clear();
    }
}