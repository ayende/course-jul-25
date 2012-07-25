using System;
using System.Collections.Generic;

namespace MediaMind.Course.Models
{
	public class Account
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Type { get; set; }
		public virtual string CsUser { get; set; }
		public virtual DateTime CreationDate { get; set; }
		public virtual bool IsActive { get; set; }
		public virtual Office Office { get; set; }
		public virtual ICollection<Campaign> Campaigns { get; set; }

		public Account()
		{
			Campaigns = new HashSet<Campaign>();
		}
	}
}