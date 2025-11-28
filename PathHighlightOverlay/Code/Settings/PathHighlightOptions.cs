using ColossalFramework.UI;
using ICities;
using PathHighlightOverlay.Code.Utility;
using UnityEngine;

namespace PathHighlightOverlay.Code.Settings
{
public class PathHighlightOptions : IUserMod
{
    public string Name => "Path Highlight Overlay";
    public string Description => "Highlights all pedestrian paths (including invisible ones).";

    private Texture2D _hueTexture;
    private UISlider _hueSlider;
    private UITextureSprite _hueBar;

    public void OnSettingsUI(UIHelperBase helper)
    {
        var group = helper.AddGroup("Path Highlight Overlay") as UIHelper;
        if (group == null) return;

        var panel = group.self as UIPanel;
        if (panel == null) return;

        var label = panel.AddUIComponent<UILabel>();
        label.text = "Highlight color";

        // Load gradient texture
        if (_hueTexture == null)
            _hueTexture = ModResources.LoadTexture("HueGradient.png");

        float initialHue = PathHighlightSettingsLoader.Config.Hue;

        // Create slider
        var sliderObj = group.AddSlider(
            "Highlight hue",
            0f,
            1f,
            0.01f,
            initialHue,
            OnSliderValueChanged);   
        _hueSlider = sliderObj as UISlider;
        if (_hueSlider == null) return;
        
        _hueSlider.backgroundSprite = string.Empty;
        _hueSlider.color = Color.white; 

        
        if (_hueTexture != null)
        {
            _hueSlider.clipChildren = true; 
            _hueBar = _hueSlider.AddUIComponent<UITextureSprite>();
            _hueBar.texture = _hueTexture;
            
            _hueBar.size = _hueSlider.size;
            _hueBar.relativePosition = Vector3.zero;
            _hueBar.zOrder = 0;

            // Ensure the thumb is above the gradient
            if (_hueSlider.thumbObject != null)
            {
                _hueSlider.thumbObject.zOrder = _hueBar.zOrder + 1;
            }
        }
        
        group.AddButton("Reset to default", () =>
        {
            PathHighlightSettingsLoader.Reset();
            float resetHue = PathHighlightSettingsLoader.Config.Hue;
            _hueSlider.value = resetHue;
        });
    }

    private void OnSliderValueChanged(float value)
    {
        PathHighlightSettingsLoader.Config.Hue = value;
        PathHighlightSettingsLoader.Save();
    }

    private Color ColorFromHue(float hue)
    {
        return Color.HSVToRGB(hue, 1f, 1f);
    }
}

}
