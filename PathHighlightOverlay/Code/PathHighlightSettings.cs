using ColossalFramework;
using UnityEngine;

namespace PathHighlightOverlay.Code
{
    public static class PathHighlightSettings
    {
        public const string SETTINGS_FILE_NAME = "PathHighlightOverlay";
        
        private static readonly SavedInt HighlightR =
            new SavedInt("HighlightR", SETTINGS_FILE_NAME, 0, true);   // cyan: R=0
        private static readonly SavedInt HighlightG =
            new SavedInt("HighlightG", SETTINGS_FILE_NAME, 255, true); // cyan: G=255
        private static readonly SavedInt HighlightB =
            new SavedInt("HighlightB", SETTINGS_FILE_NAME, 255, true); // cyan: B=255
        private static readonly SavedInt HighlightA =
            new SavedInt("HighlightA", SETTINGS_FILE_NAME, 255, true); // full alpha

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