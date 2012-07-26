using System.Linq;
using System.Web.Mvc;
using MediaMind.Course.Models;
using NHibernate.Linq;

namespace MediaMind.Course.Controllers
{
    public class FunController : NHibernateController
    {
         public ActionResult Do()
         {
             var campaigns = Session.Query<Campaign>()
                 .Select(c => new
                 {
                     c.Name,
                     AccountName = c.Account.Name,
                     AdCount = c.Ads.Count
                 })
                 .Take(100)
                 .ToList();

             return Json(campaigns);
         }
    }
}