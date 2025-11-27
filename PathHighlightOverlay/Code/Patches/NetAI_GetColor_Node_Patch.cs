using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace PathHighlightOverlay.Code.Patches
{
    [HarmonyPatch]
    public static class NetAI_GetColor_Node_Patch
    {
        static MethodBase TargetMethod()
        {
            return AccessTools.Method(
                typeof(NetAI),
                "GetColor",
                new Type[]
                {
                    typeof(ushort),
                    typeof(NetNode).MakeByRefType(),
                    typeof(InfoManager.InfoMode),
                    typeof(InfoManager.SubInfoMode)
                }
            );
        }

        public static void Postfix(
            ushort nodeID,
            ref NetNode data,
            InfoManager.InfoMode infoMode,
            InfoManager.SubInfoMode subInfoMode,
            ref Color __result
        )
        {
            if (infoMode != InfoManager.InfoMode.Traffic)
                return;

            NetInfo info = data.Info;
            if (info == null)
                return;

            if (!NetAI_GetColor_Segment_Patch.IsPurePedestrianPath(info))
                return;

            __result = new Color(0f, 1f, 1f, 1f); // cyan
        }
    }
}