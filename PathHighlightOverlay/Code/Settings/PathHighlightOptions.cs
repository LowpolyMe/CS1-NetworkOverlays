using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using PathHighlightOverlay.Code.Utility;
using UnityEngine;

namespace PathHighlightOverlay.Code.Settings
{
    public class PathHighlightOptions: IUserMod
    { 
       
        public string Name => "Path Highlight Overlay";
        private Color _currentColor;
        public string Description =>
        "Highlights all pedestrian paths (including invisible ones).";

        //private UIPanel _colorPreview;
        private Texture2D _hueTexture;
        private UISlider _hueSlider;
        
        public void OnSettingsUI(UIHelperBase helper)
        {
            var group = helper.AddGroup("Path Highlight Overlay") as UIHelper;
            if (group == null) return;

            var panel = group.self as UIPanel;
            if (panel == null) return;

            // Label
            var label = panel.AddUIComponent<UILabel>();
            label.text = "Highlight color";
            
            if (_hueTexture == null)
                _hueTexture = ModResources.LoadTexture("HueGradient.png");

            // Get initial color
            float initialHue = PathHighlightSettingsLoader.Config.Hue;
            var initialColor = ColorFromHue(initialHue);
            
            
            // Slider
            var sliderObj = group.AddSlider("Highlight hue", 0f, 1f, 0.01f, initialHue, OnSliderValueChanged);
            _hueSlider = sliderObj as UISlider;
            UpdateSliderTrackColor(initialHue);
            
            if (_hueTexture != null && _hueSlider != null)
            {
                var hueBar = panel.AddUIComponent<UITextureSprite>();
                hueBar.texture = _hueTexture;
                hueBar.size = new Vector2(_hueSlider.size.x, 32f);
            }
            

            // Reset button
            /*group.AddButton("Reset to default", () => {
                PathHighlightSettingsLoader.Reset();
            });*/
        }
        private void OnSliderValueChanged(float value)
        {
            UpdateHue(value);
            UpdateSliderTrackColor(value); 
        }
        
        private void UpdateSliderTrackColor(float hue)
        {
            Color color = ColorFromHue(hue);
            if (_hueSlider != null)
            {
                _hueSlider.color = color; // this tints the background
            }

            if (_hueSlider?.thumbObject is UISprite thumb)
            {
                thumb.color = color; // Optional: knob color
            }
        }
        private void UpdateHue(float value)
        {
            PathHighlightSettingsLoader.Config.Hue = value;
            PathHighlightSettingsLoader.Save();
            _currentColor = ColorFromHue(value);
        }

        private Color ColorFromHue(float hue)
        {
            return Color.HSVToRGB(hue, 1f, 1f);
        }
        
    }
}
