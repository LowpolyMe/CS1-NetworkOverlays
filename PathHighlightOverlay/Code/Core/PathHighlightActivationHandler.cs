using UnityEngine;

namespace PathHighlightOverlay.Code.Core
{
    public class PathHighlightActivationHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F9))
            {
                PathHighlightManager.IsEnabled = !PathHighlightManager.IsEnabled;
            }
        }
        //todo: also activate when current tool is road draw tool IF enabled in options menu
        //todo: also activate when current tool is pedestrian path draw tool IF enabled in options menu
        //todo: also activate when current tool is ANY network draw tool IF enabled in options menu

    }
}