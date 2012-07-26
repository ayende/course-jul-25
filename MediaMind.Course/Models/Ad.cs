namespace MediaMind.Course.Models
{
    public class Ad
    {
        public virtual string Type
        {
            get
            {
                if (Data is VideoStripImageAdData)
                    return "Video Strip Image";
                if (Data is StandardBanner)
                    return "Standard Banner";

                return "no idea";
            }
        }
        public virtual Campaign Campaign { get; set; }
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual SharedAdData Data { get; set; }
    }

    public abstract class SharedAdData
    {
        public virtual long Id { get; set; }
        public virtual int SmartResource { get; set; }
    }

    public class VideoStripImageAdData : SharedAdData
    {
        public virtual string DefaultImage { get; set; }
    }

    public class StandardBanner : SharedAdData
    {
        public virtual string Size { get; set; }
    }
}