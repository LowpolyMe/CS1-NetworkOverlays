using System.Xml.Serialization;

namespace PathHighlightOverlay.Code.Settings
{
    [XmlRoot("PathHighlightSettings")]
    public class PathHighlightConfig
    {
        public int Version { get; set; } = 1;
        public float Hue { get; set; } = 0.85f; 
        //public float PedestrianPathsHue { get; set; } = 0.85f; 
        //public float RoadsHue { get; set; } = 0.85f; 
        //public float HighwaysHue { get; set; } = 0.85f; 
        //public float TrainTracksHue { get; set; } = 0.85f; 
        //...
    }
}