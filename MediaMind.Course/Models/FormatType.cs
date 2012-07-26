namespace MediaMind.Course.Models
{
    public class FormatType
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }

        public virtual FormatType Clone()
        {
            return new FormatType
            {
                Name = Name,
                Id = Id
            };
        }
    }
}