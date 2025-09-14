using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.IO;

namespace Janice.Core.Services
{
    public class LoggingService : Janice.Core.Services.Interfaces.ILoggingService, IDisposable
    {
        private Logger _logger;
        private readonly List<string> _fileSinks = new();
        private bool _consoleSinkEnabled = false;
        private bool _cliSinkEnabled = false;
        private LoggerConfiguration _config;
        private readonly string _defaultLogFile = Path.Combine("Data", $"events-{DateTime.UtcNow:yyyyMMdd}.log");

        public LoggingService()
        {
            _config = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(_defaultLogFile, rollingInterval: RollingInterval.Day);
            _logger = _config.CreateLogger();
            _fileSinks.Add(_defaultLogFile);
        }

        public void LogInformation(string message) => _logger.Information(message);
        public void LogWarning(string message) => _logger.Warning(message);
        public void LogError(string message) => _logger.Error(message);
        public void LogDebug(string message) => _logger.Debug(message);
        public void LogEvent(string message) => _logger.Information($"EVENT: {message}");

        public void AddFileSink(string filePath)
        {
            if (!_fileSinks.Contains(filePath))
            {
                _fileSinks.Add(filePath);
                _config = _config.WriteTo.File(filePath, rollingInterval: RollingInterval.Day);
                _logger = _config.CreateLogger();
            }
        }

        public void RemoveFileSink(string filePath)
        {
            if (_fileSinks.Contains(filePath))
            {
                _fileSinks.Remove(filePath);
                // Rebuild config without this file sink
                _config = new LoggerConfiguration().MinimumLevel.Debug();
                foreach (var file in _fileSinks)
                {
                    _config = _config.WriteTo.File(file, rollingInterval: RollingInterval.Day);
                }
                if (_consoleSinkEnabled)
                    _config = _config.WriteTo.Console();
                if (_cliSinkEnabled)
                    _config = _config.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}");
                _logger = _config.CreateLogger();
            }
        }

        public void EnableConsoleSink()
        {
            if (!_consoleSinkEnabled)
            {
                _consoleSinkEnabled = true;
                _config = _config.WriteTo.Console();
                _logger = _config.CreateLogger();
            }
        }

        public void DisableConsoleSink()
        {
            if (_consoleSinkEnabled)
            {
                _consoleSinkEnabled = false;
                // Rebuild config without console sink
                _config = new LoggerConfiguration().MinimumLevel.Debug();
                foreach (var file in _fileSinks)
                {
                    _config = _config.WriteTo.File(file, rollingInterval: RollingInterval.Day);
                }
                if (_cliSinkEnabled)
                    _config = _config.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}");
                _logger = _config.CreateLogger();
            }
        }

        public void EnableCliSink()
        {
            if (!_cliSinkEnabled)
            {
                _cliSinkEnabled = true;
                _config = _config.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}");
                _logger = _config.CreateLogger();
            }
        }

        public void DisableCliSink()
        {
            if (_cliSinkEnabled)
            {
                _cliSinkEnabled = false;
                // Rebuild config without CLI sink
                _config = new LoggerConfiguration().MinimumLevel.Debug();
                foreach (var file in _fileSinks)
                {
                    _config = _config.WriteTo.File(file, rollingInterval: RollingInterval.Day);
                }
                if (_consoleSinkEnabled)
                    _config = _config.WriteTo.Console();
                _logger = _config.CreateLogger();
            }
        }

        public void Dispose()
        {
            _logger?.Dispose();
        }
    }
}
