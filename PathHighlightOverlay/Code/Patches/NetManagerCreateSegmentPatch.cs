using HarmonyLib;
using ColossalFramework.Math;
using UnityEngine;
using System.Reflection;
using System;


namespace PathHighlightOverlay.Code.Patches
{
    [HarmonyPatch]
    public static class NetManagerCreateSegmentPatch
    {
        static MethodBase TargetMethod()
        {
            var args = new[]
            {
                typeof(ushort).MakeByRefType(), 
                typeof(Randomizer).MakeByRefType(), 
                typeof(NetInfo), 
                typeof(TreeInfo), 
                typeof(ushort),
                typeof(ushort), 
                typeof(Vector3), 
                typeof(Vector3), 
                typeof(uint), 
                typeof(uint), 
                typeof(bool) 
            };

            return AccessTools.Method(typeof(NetManager), "CreateSegment", args);
        }

      
        static void Postfix(
            ref ushort segment,
            ref Randomizer randomizer,
            NetInfo info,
            TreeInfo treeInfo,
            ushort startNode,
            ushort endNode,
            Vector3 startDirection,
            Vector3 endDirection,
            uint buildIndex,
            uint modifiedIndex,
            bool invert,
            bool __result)
        {
            if (!__result || info == null)
                return;

            var ai = info.m_netAI;
            if (ai is PedestrianPathAI || ai is PedestrianWayAI)
            {
                PathHighlightManager.Instance?.OnSegmentCreated(segment);
            }
        }
    }


        [HarmonyPatch]
        public static class NetManagerReleaseSegmentPatch
        {
            // Pick the overload: void ReleaseSegment(ushort segment, bool keepNodes)
            static MethodBase TargetMethod()
            {
                return typeof(NetManager).GetMethod(
                    "ReleaseSegment",
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new[] { typeof(ushort), typeof(bool) },
                    null);
            }

            // We only need the segment id, keepNodes is irrelevant for us
            static void Prefix(ushort segment, bool keepNodes)
            {
                try
                {
                    PathHighlightManager.Instance?.OnSegmentReleased(segment);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[PathHighlightOverlay] Error in ReleaseSegment prefix: {e}");
                }
            }
        }
    }


