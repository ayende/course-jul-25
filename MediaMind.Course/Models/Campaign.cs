using System.Collections.Generic;

namespace MediaMind.Course.Models
{
	public class Campaign
	{
		public virtual long Id { get; set; }
		public virtual Account Account { get; set; }
		public virtual string Name { get; set; }
		public virtual ICollection<Ad> Ads { get; set; }

		public Campaign()
		{
			Ads = new HashSet<Ad>();
		}
	}

	public class Ad
	{
		public virtual Campaign Campaign { get; set; }
		public virtual long Id { get; set; }
		public virtual string Name { get; set; }
	}
}