using System;
using UnityEngine;

namespace NetworkHighlightOverlay.Code.ModOptions
{
    public static class ModSettings
    {
        private static readonly Config _config;
        
        
        public static event Action<Config> SettingsChanged;

        static ModSettings()
        {
            _config = SettingsLoader.Load();
        }

        public static float PedestrianPathsHue
        {
            get => _config.PedestrianPathsHue;
            set
            {
                if (Mathf.Approximately(_config.PedestrianPathsHue, value))
                    return;

                _config.PedestrianPathsHue = value;
                SettingsLoader.Save(_config);
                
                SettingsChanged?.Invoke(_config);
            }
        }
        
        public static Color PedestrianPathColor
        {
            get => Color.HSVToRGB(PedestrianPathsHue, 1f, 1f);

            set
            {
                Color.RGBToHSV(value, out float newHue, out _, out _);
                PedestrianPathsHue = newHue;
            }
        }

        public static void ResetToDefaults()
        {
            PedestrianPathsHue = 0.85f;

            SettingsLoader.Save(_config);
            
            SettingsChanged?.Invoke(_config);
        }
    }
}