using HarmonyLib;
using ColossalFramework;
using PathHighlightOverlay.Code;
using PathHighlightOverlay.Code.Core;

namespace PathHighlightOverlay.Code.Patches
{
    [HarmonyPatch(typeof(ToolBase), "RenderOverlay")]
    public static class ToolRenderOverlayPatch
    {
        static void Postfix(RenderManager.CameraInfo cameraInfo)
        {
            PathHighlightManager.Instance?.RenderIfActive(cameraInfo);
        }
    }
}