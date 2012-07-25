using System;
using System.Web.Mvc;
using MediaMind.Course.Models;

namespace MediaMind.Course.Controllers
{
	public class HomeController : NHibernateController
	{
		public ActionResult Index()
		{
			return Json(SessionFactory != null);
		}

		public ActionResult NewAccount(string name)
		{
			for (int i = 0; i < 5; i++)
			{
				var account = new Account
				{
					CreationDate = DateTime.Now,
					IsActive = true,
					Name = name,
					CsUser = "something",
					Type = "Agency"
				};
				Session.Save(account);
			}

			return Json(new {Created = true, });
		}
	}
}