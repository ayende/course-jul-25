using System;

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
	}
}