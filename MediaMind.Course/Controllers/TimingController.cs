using System.Web.Mvc;

namespace MediaMind.Course.Controllers
{
    public class TimingController :NHibernateController
    {
         public ActionResult Took()
         {
             return Json(sp.ElapsedMilliseconds);
         }
    }
}