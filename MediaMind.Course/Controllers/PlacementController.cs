using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MediaMind.Course.Models;

namespace MediaMind.Course.Controllers
{
    public class PlacementController : NHibernateController
    {
         public ActionResult Create()
         {
             var placement = new Placement
             {
                 Name = "test",
                 Type = PlacementType.Banner,
                 Banner = new PlacementBannerData
                 {
                     Color = "red",
                     Size = 5
                 },
                 Attributes = new Dictionary<string, string>
                 {
                     {"BannerSize", "250x120"},
                     {"Date", DateTime.Now.ToString("g")}
                 }
             };
             return Json(Session.Save(placement));
         }

        public ActionResult Change(int id)
        {
            var placement = Session.Get<Placement>(id);
            placement.Type =PlacementType.OutOfBanner;
            placement.Banner = null;
            placement.OutOfBanner = new PlacementOutOfBannerData
            {
                Position = 51
            };
            return Json("changed");
        }
    }
}