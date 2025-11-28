using ColossalFramework.Math;
using UnityEngine;

namespace PathHighlightOverlay.Data
{
    public struct PathSegmentGeometry
    {
        public readonly ushort SegmentId;
        public readonly Bezier3 Bezier;
        public readonly float StartWidth;
        public readonly float EndWidth;

        public PathSegmentGeometry(ushort segmentId, ref NetSegment segment)
        {
            SegmentId = segmentId;

            NetManager netManager = NetManager.instance;

            ushort startNodeId = segment.m_startNode;
            ushort endNodeId   = segment.m_endNode;

            ref NetNode startNode = ref netManager.m_nodes.m_buffer[startNodeId];
            ref NetNode endNode   = ref netManager.m_nodes.m_buffer[endNodeId];

            Vector3 startPos = startNode.m_position;
            Vector3 endPos   = endNode.m_position;
            Vector3 startDir = segment.m_startDirection;
            Vector3 endDir   = segment.m_endDirection;

            // This is the same pattern the game uses to build the network Bezier:
            NetSegment.CalculateMiddlePoints(
                startPos, startDir,
                endPos,   endDir,
                smoothStart: true,
                smoothEnd:   true,
                out Vector3 middle1,
                out Vector3 middle2);

            Bezier = new Bezier3(startPos, middle1, middle2, endPos);

            // For paths, width is usually constant, but we still expose start/end
            float baseWidth = segment.Info.m_halfWidth * 2f;
            StartWidth = baseWidth;
            EndWidth   = baseWidth;
        }
    }
}