using System.Web.Mvc;

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