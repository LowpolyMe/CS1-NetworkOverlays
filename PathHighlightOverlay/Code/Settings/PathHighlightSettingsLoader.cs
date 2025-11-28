using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace PathHighlightOverlay.Code.Settings
{
    public class PathHighlightSettingsLoader
    {
        private static PathHighlightConfig _cached;

        private static string ConfigPath
        {
            get
            {
                string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                string modDirectory = Path.Combine(basePath, @"Colossal Order\Cities_Skylines\Addons\Mods\PathHighlightOverlay\Settings");
                return Path.Combine(modDirectory, "PathHighlightSettings.xml");
            }
        }

        public static PathHighlightConfig Config
        {
            get
            {
                Debug.Log($"[PathHighlightOverlay] Config path: {ConfigPath}");
                if (_cached != null)
                    return _cached;

                if (File.Exists(ConfigPath))
                {
                    try
                    {
                        using (FileStream stream = File.OpenRead(ConfigPath))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(PathHighlightConfig));
                            _cached = (PathHighlightConfig)serializer.Deserialize(stream);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogWarning($"[PathHighlightOverlay] Failed to load config: {ex}");
                        _cached = new PathHighlightConfig(); // fallback
                    }
                }
                else
                {
                    _cached = new PathHighlightConfig(); // first-time default
                }

                return _cached;
            }
        }

        public static void Save()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PathHighlightConfig));

                // Ensure directory exists
                string directory = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (StreamWriter writer = new StreamWriter(ConfigPath))
                {
                    serializer.Serialize(writer, _cached);
                }

                Debug.Log("[PathHighlightOverlay] Settings saved.");
            }
            catch (IOException ex)
            {
                Debug.LogError($"[PathHighlightOverlay] Failed to save config: {ex}");
            }
        }
        public static void Reset()
        {
            _cached = new PathHighlightConfig();
            Save();
        }
    }
}