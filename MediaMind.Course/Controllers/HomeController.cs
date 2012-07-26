using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Web.Mvc;
using MediaMind.Course.Models;
using NHibernate;
using NHibernate.Linq;

namespace MediaMind.Course.Controllers
{
	public class HomeController : NHibernateController
	{
		public ActionResult Index()
		{
			return Json(SessionFactory != null);
		}
		public ActionResult NoLock(long id)
		{
			Session.Get<Ad>(id, LockMode.None);
			return Json("okay");
		}
		public ActionResult AccountFromAd(int id)
		{
			return Json(Session.Query<Ad>()
			            	.Where(x => x.Id == id)
			            	.Select(x => x.Campaign.Account.Id)
			            	.First()
				);
		}

		public ActionResult Nice(int id)
		{
			var accounts = Session.Query<Account>()
				.Fetch(x => x.Office)
				.Where(x => x.Id == id)
				.ToFuture();

			var campaigns = Session.Query<Campaign>()
				.Where(x => x.Account.Id == id)
				.ToFuture();

			var ads = Session.Query<Ad>()
				.Where(x => x.Campaign.Account.Id == id)
				.ToFuture();


			var account = accounts.Single();
			return Json(
				new
				{
					OfficeName = account.Office.Name,
					AccountName = account.Name,
					Campaigns = campaigns.Select(x => x.Name).ToArray(),
					Ads = ads.Select(x => x.Name).ToArray()
				});
		}

		public ActionResult StupidKillDatbaseHarshly(int id)
		{
			var accounts = Session.Query<Account>()
				.Fetch(x => x.Office)
				.Where(x => x.Id == id)
				.ToFuture();

			Session.Query<Account>()
				.Where(x => x.Id == id)
				.FetchMany(x => x.Campaigns)
				.ToFuture();

			Session.Query<Campaign>()
				.FetchMany(x => x.Ads)
				.Where(x => x.Account.Id == id)
				.ToFuture();


			var account = accounts.Single();
			return Json(
				new
				{
					OfficeName = account.Office.Name,
					AccountName = account.Name,
					Campaigns = account.Campaigns.Select(x => x.Name).ToArray(),
					Ads = account.Campaigns.SelectMany(x => x.Ads).Select(x => x.Name).ToArray()
				}
				);

		}

		public ActionResult AccountWithQuery(int id)
		{
			var account = Session.Query<Account>()
				.Fetch(x => x.Office)
				.First(x => x.Id == id);

			return Json(new
			{
				AccountName = account.Name,
				OfficeName = account.Office.Name
			});
		}

		public ActionResult Two(int id)
		{
			var account = Session.Query<Account>()
				.First(x => x.Id == id);
			var account1 = Session.Get<Account>(id);

			return Json(new
			{
				Same = ReferenceEquals(account1, account)
			});
		}

		public ActionResult AccountWithRecentCampaign(int id)
		{
			var accountFuture = Session.Query<Account>()
				.Where(x => x.Id == id)
				.ToFuture();

			var campaigns = Session.Query<Campaign>()
				.Where(x => x.Account.Id == id)
				.OrderByDescending(x => x.Id)
				.Take(3)
				.ToFuture();

			return Json(new
			{
				accountFuture.Single().Name,
				Campaigns = campaigns.Select(x => x.Name).ToArray()
			});
		}

		public ActionResult LoadAccount(int id)
		{
			var account = Session.Get<Account>(id);

			return Json(new
			{
				AdsCount = account.Campaigns.Sum(x=>x.Ads.Count)
			});
		}

		public ActionResult AddCampaign(int id)
		{
			var account = Session.Load<Account>(id);
			var c = new Campaign
			{
				Name = "cooler ",
				Account = account
			};
			Session.Save(c);
			return Json("okay");
		}

		public ActionResult CampaignByContact(long id, int start, int pageSize)
		{
			var q = Session.Query<Campaign>()
				.Take(pageSize)
				.Skip(start)
				.Where(x => x.Contacts.Any(y => y.Id == id))
				.Select(x => new { x.Id, x.Name });

			return Json(q.ToList());
		}

		public ActionResult AddContactToCampaign(long id, long contactId)
		{
			var contact = Session.Load<Contact>(contactId);
			var campaign = Session.Get<Campaign>(id);
			campaign.Contacts.Add(contact);

			return Json("ok");
		}

		public ActionResult NewAccount(string name)
		{
			var office = new Office { Name = "Office for " + name };
			Session.Save(office);


			for (int i = 0; i < 5; i++)
			{
				var contact = new Contact
				{
					Name = "test " + i
				};
				Session.Save(contact);
				var account = new Account
				{
					CreationDate = DateTime.Now,
					IsActive = true,
					Name = name,
					CsUser = "something",
					Type = "Agency",
					Office = office
				};

				Session.Save(account);

				for (int j = 0; j < 10; j++)
				{
					var c = new Campaign
					{
						Name = "cool " + i + " " + j,
						Account = account,
						Contacts = { contact }
					};
					Session.Save(c);

                    //for (int k = 0; k < 20; k++)
                    //{
                    //    var ad = new Ad
                    //    {
                    //        Campaign = c,
                    //        Name = "amazing"
                    //    };
                    //    Session.Save(ad);
                    //}
				}
			}

			return Json(new { Created = true, });
		}
	}
}