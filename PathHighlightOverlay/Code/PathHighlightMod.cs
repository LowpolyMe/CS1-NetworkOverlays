using ICities;

namespace PathHighlightOverlay
{
    public class PathHighlightMod : IUserMod
    {
        public string Name => "Path Highlight Overlay";
        public string Description =>
            "Highlights all pedestrian paths (including invisible ones).";
    }
}