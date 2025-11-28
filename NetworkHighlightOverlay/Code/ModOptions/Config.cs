using System.Xml.Serialization;

namespace NetworkHighlightOverlay.Code.ModOptions
{
    [XmlRoot("NetworkNetworkHighlightSettings")]
    public class Config
    {
        public int Version { get; set; } = 1;
        public float PedestrianPathsHue { get; set; } = 0.85f; 
        //public float RoadsHue { get; set; } = 0.85f; 
        //public float HighwaysHue { get; set; } = 0.85f; 
        //public float TrainTracksHue { get; set; } = 0.85f; 
        //...
    }
}