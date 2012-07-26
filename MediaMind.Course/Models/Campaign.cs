using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MediaMind.Course.Models
{
	public class Campaign
	{
		public virtual long Id { get; set; }
		public virtual Account Account { get; set; }
		public virtual string Name { get; set; }
		public virtual ICollection<Ad> Ads { get; set; }
		public virtual ICollection<Contact> Contacts { get; set; }

		public Campaign()
		{
			Ads = new HashSet<Ad>();
			Contacts = new HashSet<Contact>();
		}
	}

	public class Ad
	{
		public virtual Campaign Campaign { get; set; }
		public virtual long Id { get; set; }
		public virtual string Name { get; set; }
	}

    public class PositiveAd : Ad
    {
        public virtual string WhyThisIsAwesome { set; get; }
    }

    public class NegativeAd : Ad
    {
        public virtual string WhatThatSucks { set; get; }
    }
}