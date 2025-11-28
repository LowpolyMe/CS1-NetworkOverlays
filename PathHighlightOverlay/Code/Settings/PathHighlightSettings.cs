using ColossalFramework;
using UnityEngine;

namespace PathHighlightOverlay.Code.Settings
{
    public static class PathHighlightSettings
    {
        //todo GameSettings: 'PathHighlightOverlay' is not found or cannot be loaded. Make sure to call GameSettings.AddSettingsFile() to register a settings file before using SavedValue(). 
        //todo store only HSV value and use Color.HSVToRGB(storedValue, 1f, 1f);
        public const string SETTINGS_FILE_NAME = "PathHighlightOverlay";
        
        private static readonly SavedInt HighlightR =
            new SavedInt("HighlightR", SETTINGS_FILE_NAME, 0, true);   
        private static readonly SavedInt HighlightG =
            new SavedInt("HighlightG", SETTINGS_FILE_NAME, 255, true); 
        private static readonly SavedInt HighlightB =
            new SavedInt("HighlightB", SETTINGS_FILE_NAME, 255, true); 
        private static readonly SavedInt HighlightA =
            new SavedInt("HighlightA", SETTINGS_FILE_NAME, 255, true); 

        public static Color HighlightColor
        {
            get => new Color(
                HighlightR.value / 255f,
                HighlightG.value / 255f,
                HighlightB.value / 255f,
                HighlightA.value / 255f);

            set
            {
                HighlightR.value = Mathf.Clamp(Mathf.RoundToInt(value.r * 255f), 0, 255);
                HighlightG.value = Mathf.Clamp(Mathf.RoundToInt(value.g * 255f), 0, 255);
                HighlightB.value = Mathf.Clamp(Mathf.RoundToInt(value.b * 255f), 0, 255);
                HighlightA.value = Mathf.Clamp(Mathf.RoundToInt(value.a * 255f), 0, 255);
            }
        }

        public static void ResetToDefault()
        {
            HighlightColor = new Color(1f, 0f, 1f, 1f);
        }
    }
}