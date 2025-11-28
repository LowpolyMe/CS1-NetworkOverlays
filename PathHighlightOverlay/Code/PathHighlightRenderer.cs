using ColossalFramework;
using UnityEngine;

namespace PathHighlightOverlay.Code
{
    public class PathHighlightRenderer : MonoBehaviour
    {
        private void OnPostRender()
        {
            var cam = Camera.current;
            if (cam == null || cam != Camera.main)
                return;

            // Most common constructor: CameraInfo(Camera)
            // If your reference uses a different one, match that.
            var cameraInfo = new RenderManager.CameraInfo();

            PathHighlightManager.Instance.RenderIfActive(cameraInfo);
        }
    }
}