using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace PathHighlightOverlay.Code.Patches
{
    [HarmonyPatch]
    public static class NetAI_GetColor_Segment_Patch
    {
        private static readonly Dictionary<NetInfo, bool> _purePedCache = new Dictionary<NetInfo, bool>();

        static MethodBase TargetMethod()
        {
            return AccessTools.Method(
                typeof(NetAI),
                "GetColor",
                new Type[]
                {
                    typeof(ushort),
                    typeof(NetSegment).MakeByRefType(),
                    typeof(InfoManager.InfoMode),
                    typeof(InfoManager.SubInfoMode)
                }
            );
        }

        public static void Postfix(
            ushort segmentID,
            ref NetSegment data,
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

            if (!IsPurePedestrianPath(info))
                return;

            __result = new Color(0f, 1f, 1f, 1f); // cyan
        }

        internal static bool IsPurePedestrianPath(NetInfo info)
        {
            if (info == null || info.m_lanes == null)
                return false;

            if (_purePedCache.TryGetValue(info, out bool cached))
                return cached;

            bool hasPedLane = false;
            bool hasRealVehicleLane = false;

            foreach (var lane in info.m_lanes)
            {
                var type = lane.m_laneType;

                if ((type & NetInfo.LaneType.Pedestrian) != 0)
                    hasPedLane = true;

                // Only treat it as "non-ped" if it's a vehicle lane with an actual vehicle type
                if ((type & NetInfo.LaneType.Vehicle) != 0 &&
                    lane.m_vehicleType != VehicleInfo.VehicleType.None)
                {
                    hasRealVehicleLane = true;
                }

                // TransportVehicle is always non-ped for our purposes
                if ((type & NetInfo.LaneType.TransportVehicle) != 0)
                {
                    hasRealVehicleLane = true;
                }
            }

            bool result = hasPedLane && !hasRealVehicleLane;
            _purePedCache[info] = result;
            return result;
        }

    }
}
