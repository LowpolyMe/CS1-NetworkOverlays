using ColossalFramework;
using UnityEngine;

namespace PathHighlightOverlay.Code
{
    public class PathHighlightRenderer : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F9))
            {
                PathHighlightManager.IsEnabled = !PathHighlightManager.IsEnabled;
                Debug.Log($"[PathHighlightOverlay] Toggled: {(PathHighlightManager.IsEnabled ? "ON" : "OFF")}");
            }
        }

    }
}