using System;
using Janice.Core.Models;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Janice.Core.Services.Interfaces;

namespace Janice.Core.Services
{
    public class ConfigService : IConfigService, IDisposable
    {
        private readonly string _configPath;
        private FileSystemWatcher? _watcher;
        private JanetConfig _config;
        private readonly object _lock = new();
        public event EventHandler? ConfigReloaded;

        public ConfigService(string configPath)
        {
            _configPath = configPath;
            _config = LoadConfig();
            SetupWatcher();
        }

        public JanetConfig Config => _config;
        public string OllamaApiUrl => _config.OllamaApiUrl;
        public string DefaultModel => _config.DefaultModel;
        public int MaxTokens => _config.MaxTokens;
        public double Temperature => _config.Temperature;
        public double TopP => _config.TopP;
        public int N => _config.N;
        public bool Stream => _config.Stream;
        public int TimeoutSeconds => _config.TimeoutSeconds;

        public async Task ReloadAsync()
        {
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    _config = LoadConfig();
                    ConfigReloaded?.Invoke(this, EventArgs.Empty);
                }
            });
        }

        public async Task UpdateConfigAsync(Action<JanetConfig> updateAction)
        {
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    updateAction(_config);
                    SaveConfig(_config);
                    ConfigReloaded?.Invoke(this, EventArgs.Empty);
                }
            });
        }

        private JanetConfig LoadConfig()
        {
            if (!File.Exists(_configPath))
            {
                var defaultConfig = new JanetConfig();
                SaveConfig(defaultConfig);
                return defaultConfig;
            }
            var yaml = File.ReadAllText(_configPath);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            return deserializer.Deserialize<JanetConfig>(yaml) ?? new JanetConfig();
        }

        private void SaveConfig(JanetConfig config)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            var yaml = serializer.Serialize(config);
            File.WriteAllText(_configPath, yaml);
        }

        private void SetupWatcher()
        {
            var dir = Path.GetDirectoryName(_configPath);
            var file = Path.GetFileName(_configPath);
            if (dir == null) return;
            _watcher = new FileSystemWatcher(dir, file)
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
            };
            _watcher.Changed += async (s, e) => await ReloadAsync();
            _watcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            _watcher?.Dispose();
        }
    }
}
