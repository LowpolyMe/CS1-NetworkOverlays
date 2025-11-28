using ICities;
using HarmonyLib;
using PathHighlightOverlay.Code;
using UnityEngine;

namespace PathHighlightOverlay
{
    public class PathHighlightLoading : LoadingExtensionBase
    {
        private GameObject _controllerObject;
        private static bool _patched;
        private Harmony _harmony;
        private const string HarmonyId = "com.lowpolyme.PathHighlightOverlay";

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

            if (_patched)
                return;

            _harmony = new Harmony(HarmonyId);
            _harmony.PatchAll();

            _patched = true;
        }
        public override void OnReleased()
        {
            base.OnReleased();

            if (_harmony != null)
            {
                _harmony.UnpatchAll(HarmonyId);
                _harmony = null;
            }
        }
        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            
            if (_controllerObject == null)
            {
                _controllerObject = new GameObject("PathHighlightRenderer");
                _controllerObject.AddComponent<PathHighlightRenderer>();
                GameObject.DontDestroyOnLoad(_controllerObject);
            }
            
            PathHighlightManager.Instance.RebuildCache();

        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();

            if (_controllerObject != null)
            {
                Object.Destroy(_controllerObject);
                _controllerObject = null;
            }

            PathHighlightManager.Instance.Clear();
        }
        
    }
}