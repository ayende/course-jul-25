using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI;
using NHibernate;

namespace MediaMind.Course.BusinessRules
{
	public abstract class AbstractValidator<T> : IValidateBusinessRules
		where T : class
	{


		public virtual void OnSave(T instance)
		{
			
		}
		public void OnDelete(T instance) { }


		public void OnSave(object instance)
		{
			var foo = instance as T;
			if (foo == null)
				return;
			OnSave(foo);
		}

		public Type ForType
		{
			get { return typeof(T); }
		}
	}

	public interface IValidateBusinessRules
	{
		void OnSave(object instance);
		Type ForType { get; }
	}
}