using ICities;
using HarmonyLib;

namespace PathHighlightOverlay
{
    public class PathHighlightLoading : LoadingExtensionBase
    {
        private static bool _patched;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

            if (_patched)
                return;

            var harmony = new Harmony("com.lowpolyme.PathHighlightOverlay");
            harmony.PatchAll();

            _patched = true;
        }
        
    }
}