using ColossalFramework;
using UnityEngine;

namespace PathHighlightOverlay.Code.Settings
{
    public static class PathHighlightSettings
    {
        //todo GameSettings: 'PathHighlightOverlay' is not found or cannot be loaded. Make sure to call GameSettings.AddSettingsFile() to register a settings file before using SavedValue(). 

        public const string SETTINGS_FILE_NAME = "PathHighlightOverlaySettings";
        
        private static readonly SavedFloat Hue =
            new SavedFloat("Hue", SETTINGS_FILE_NAME, 0f, true);

        public static float GetHue() => Hue.value;
        public static void SetHue(float hue) => Hue.value = Mathf.Clamp01(hue);

        
        public static Color HighlightColor
        {
            get
            {
                return Color.HSVToRGB(Hue.value, 1f, 1f);;
            }

            set
            {
                Color.RGBToHSV(value, out float hue, out _, out _);
                Hue.value = hue;
            }
        }
        public static void ResetToDefault()
        {
            HighlightColor = new Color(1f, 0f, 1f, 1f);
        }


    }
}