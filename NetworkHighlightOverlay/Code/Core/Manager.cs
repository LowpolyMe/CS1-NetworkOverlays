using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Math;
using NetworkHighlightOverlay.Code.ModOptions;
using UnityEngine;


namespace NetworkHighlightOverlay.Code.Core
{
    public class Manager
    {
        private Color _highlightColor;
        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled == value)
                    return;

                _isEnabled = value;

                if (_isEnabled)
                {
                    RebuildCache();
                }
                else
                {
                    Clear();
                }
            }
        }
        private static readonly Manager _instance = new Manager();
        public static Manager Instance => _instance;

        private readonly HashSet<ushort> _pathSegments = new HashSet<ushort>();

        private Manager()
        {
            _highlightColor = ModSettings.PedestrianPathColor;
            ModSettings.SettingsChanged += _ => _highlightColor = ModSettings.PedestrianPathColor;
        }

        public void Clear()
        {
            _pathSegments.Clear();
        }
        

        public void RebuildCache()
        {
            _pathSegments.Clear();
            NetManager netManager = NetManager.instance;
            var segments = netManager.m_segments;

            for (ushort i = 1; i < segments.m_size; i++)
            {
                ref NetSegment segment = ref segments.m_buffer[i];
                if ((segment.m_flags & NetSegment.Flags.Created) == 0)
                    continue;

                TryAddSegmentInternal(i, ref segment);
            }
        }

        public void OnSegmentCreated(ushort segmentId)
        {
            if (segmentId == 0)
                return;

            ref NetSegment segment = ref NetManager.instance.m_segments.m_buffer[segmentId];
            if ((segment.m_flags & NetSegment.Flags.Created) == 0)
                return;

            TryAddSegmentInternal(segmentId, ref segment);
        }


        public void OnSegmentReleased(ushort segmentId)
        {
            if (segmentId == 0)
                return;

            _pathSegments.Remove(segmentId);
        }

        private void TryAddSegmentInternal(ushort id, ref NetSegment segment)
        {
            var info = segment.Info;
            if (info == null)
                return;

            var ai = info.m_netAI;
            if (!IsPedestrianAI(ai))
                return;

            _pathSegments.Add(id);
        }

        private static bool IsPedestrianAI(NetAI ai)
        {
            return ai is PedestrianPathAI || ai is PedestrianWayAI || ai is PedestrianBridgeAI || ai is PedestrianTunnelAI;
        }


        public void RenderIfActive(RenderManager.CameraInfo cameraInfo)
        {
            if (!_isEnabled)
                return;
            
            NetManager netManager = NetManager.instance;


            foreach (ushort id in _pathSegments)
            {
                ref NetSegment segment = ref netManager.m_segments.m_buffer[id];
                
                if ((segment.m_flags & NetSegment.Flags.Created) == 0)
                    continue;
                
                RenderPedSegmentOverlay(
                    cameraInfo,
                    ref segment,
                    _highlightColor
                );
            }
        }
        private static void RenderPedSegmentOverlay(
            RenderManager.CameraInfo cameraInfo,
            ref NetSegment segment,
            Color color)
        {
            NetInfo info = segment.Info;
            if (info == null)
                return; // keeping only the null check, removing the others

            Bezier3 bezier;
            bezier.a = NetManager.instance.m_nodes.m_buffer[segment.m_startNode].m_position;
            bezier.d = NetManager.instance.m_nodes.m_buffer[segment.m_endNode].m_position;
            NetSegment.CalculateMiddlePoints(
                bezier.a, segment.m_startDirection,
                bezier.d, segment.m_endDirection,
                true, true, out bezier.b, out bezier.c);

            // same params NetTool uses
            Singleton<RenderManager>.instance.OverlayEffect.DrawBezier(
                cameraInfo,
                color,
                bezier,
                info.m_halfWidth * 2f,
                -100000f,     // no cut at start
                -100000f,     // no cut at end
                -100f,          // minY
                1280f,        // maxY
                false,
                false);
        }
    }
}