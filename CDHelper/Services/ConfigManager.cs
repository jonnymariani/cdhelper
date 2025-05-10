namespace CDHelper.Services
{
    using System;
    using System.Text.Json;

    public static class ConfigManager
    {
        private static readonly string FilePath = "config.json";
        private static Dictionary<string, JsonElement> _config = new();

        static ConfigManager()
        {
            Load();
        }

        /// <summary>
        /// Loads the configurations
        /// Carrega as configuracoes
        /// </summary>
        public static void Load()
        {
            if (!File.Exists(FilePath))
            {
                _config = new Dictionary<string, JsonElement>();
                Save();
                return;
            }

            var json = File.ReadAllText(FilePath);
            _config = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json) ?? new();
        }

        /// <summary>
        /// Saves the current configurations 
        /// Salva as configuracoes
        /// </summary>
        public static void Save()
        {
            var json = JsonSerializer.Serialize(_config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Updates or adds a configuration
        /// Atualiza ou adiciona uma configuracao
        /// </summary>
        public static void Set<T>(string key, T value)
        {
            var jsonElement = JsonSerializer.SerializeToElement(value);
            _config[key] = jsonElement;
            Save();
        }

        /// <summary>
        /// Retrieves a configuration by name
        /// Obtém uma configuração pelo nome
        /// </summary>
        public static T Get<T>(string key, T fallback = default!)
        {
            if (_config.TryGetValue(key, out var element))
            {
                try
                {
                    return element.Deserialize<T>()!;
                }
                catch
                {
                   
                }
            }

            return fallback!;
        }

        /// <summary>
        /// Checks if a configuration exists
        /// Verifica se uma configuracao existe
        /// </summary>
        public static bool Exists(string key) => _config.ContainsKey(key);

        /// <summary>
        /// Removes a configuration
        /// Remove uma configuracao
        /// </summary>
        public static bool Remove(string key)
        {
            var removed = _config.Remove(key);
            if (removed) Save();
            return removed;
        }

        /// <summary>
        /// Returns all keys
        /// Retorna todas as chaves
        /// </summary>
        public static IEnumerable<string> AllKeys() => _config.Keys;

        public static bool Toggle(string autoSearchEnabled)
        {
            var value = Get<bool>(autoSearchEnabled);

            Set<bool>(autoSearchEnabled, !value);

            return !value;
        }
    }
}
