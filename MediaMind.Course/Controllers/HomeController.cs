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
	}
}