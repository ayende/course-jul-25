using System.ComponentModel.DataAnnotations;
using MediaMind.Course.Models;
using NHibernate;

namespace MediaMind.Course.BusinessRules
{
	public class MaxContactsPerCampaignValidator 
		: AbstractValidator<Campaign>
	{
		public override void OnSave(Campaign instance)
		{
			if (NHibernateUtil.IsInitialized(instance.Contacts) == false)
				return;

			if (instance.Contacts.Count <= 5)
				return;

			throw new ValidationException("Campaign " + instance.Id  +
				" cannot have more than 5 contants.");
		}
	}
}