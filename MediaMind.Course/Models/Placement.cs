using System.Collections.Generic;

namespace MediaMind.Course.Models
{
    public class Placement
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IDictionary<string, string> Attributes { get; set; }

        public virtual PlacementType Type { get; set; }

        public virtual PlacementBannerData Banner { get; set; }
        public virtual PlacementOutOfBannerData OutOfBanner { get; set; }
    }

    public class PlacementOutOfBannerData
    {
        public virtual double Position { get; set; }

    }
    public class PlacementBannerData
    {
        public virtual int Size { get; set; }
        public virtual string Color { get; set; }
    }

    public enum PlacementType
    {
        OutOfBanner,
        Banner
    }
}