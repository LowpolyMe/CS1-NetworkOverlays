using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace PathHighlightOverlay.Code.Settings
{
    public class PathHighlightOptions: IUserMod
    {
       
        public string Name => "Path Highlight Overlay";

        public string Description =>
        "Highlights all pedestrian paths (including invisible ones).";

        

        public void OnSettingsUI(UIHelperBase helper)
        {
            var group = helper.AddGroup("Path Highlight Overlay") as UIHelper;
            if (group == null) return;

            var panel = group.self as UIPanel;
            if (panel == null) return;

            // Label
            var label = panel.AddUIComponent<UILabel>();
            label.text = "Highlight color";

            // HSB slider button
            group.AddSlider("Highlight color", 0f, 1f, 1f, 0.5f, (value) => UpdateColor(value));
            //todo: preview the currently selected color by coloring a small box next to the slider

            // Reset button
            group.AddButton("Reset to default", () => { PathHighlightSettings.ResetToDefault(); });
        }

        private void UpdateColor(float value)
        {
            Color color = Color.white;
            //todo color slider conversion
            //we don't care about saturation/brightness, we want a color hue slider and the values to be full brightness and fully saturated. We will achieve this with HSB to RGB conversion
            PathHighlightSettings.HighlightColor = color;
        }
    }
}
