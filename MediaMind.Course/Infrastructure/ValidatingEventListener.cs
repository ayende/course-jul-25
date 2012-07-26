using System;
using MediaMind.Course.BusinessRules;
using NHibernate.Event;
using System.Linq;

namespace MediaMind.Course.Infrastructure
{
	public class ValidatingEventListener
		 : IPreInsertEventListener, IPreUpdateEventListener, IPreCollectionUpdateEventListener
	{
		private readonly ILookup<Type, IValidateBusinessRules> lookup;

		public ValidatingEventListener()
		{
			lookup = typeof (ValidatingEventListener)
				.Assembly.GetTypes()
				.Where(x => !x.IsAbstract && typeof(IValidateBusinessRules).IsAssignableFrom(x))
				.Select(x=>(IValidateBusinessRules)Activator.CreateInstance(x))
				.ToLookup(x => x.ForType);
		}

		public bool OnPreInsert(PreInsertEvent @event)
		{
			var type = @event.Entity.GetType();
			Validate(type, @event.Entity);
			return false;
		}

		public bool OnPreUpdate(PreUpdateEvent @event)
		{
			var type = @event.Entity.GetType();
			Validate(type, @event.Entity);
			return false;
		}

		private void Validate(Type type, object instance)
		{
			var rules = lookup[type];
			foreach (var rule in rules)
			{
				rule.OnSave(instance);
			}
		}
		 
		public void OnPreUpdateCollection(PreCollectionUpdateEvent @event)
		{
			if (@event.AffectedOwnerOrNull == null)
				return;
			Validate(@event.AffectedOwnerOrNull.GetType(), @event.AffectedOwnerOrNull);
		}
	}
}