using System.Web.Mvc;
using MediaMind.Course.Models;
using NHibernate.Linq;
using System.Linq;

namespace MediaMind.Course.Controllers
{
    public class AdController : NHibernateController
    {
        public ActionResult NewAd()
        {
            var id = Session.Save(new Ad
            {
                Name = "Test Ad",
                Data = new StandardBanner()
                {
                    Size = "300x120",
                    SmartResource = 4
                }
            });

            return Json(id);
        }

        public ActionResult ListAds()
        {
            var list = Session.Query<Ad>().ToList();

            return Json(list.Select(x => new
            {
                x.Name, 
                x.Type,
                x.Data.SmartResource
            }).ToList());
        }


        public ActionResult ShowNewAd(long id)
        {
            var ad = Session.Get<Ad>(id);

            return Json(new
            {
                ad.Name,
                ad.Data.SmartResource,
                ((VideoStripImageAdData) ad.Data).DefaultImage
            });
        }

        public ActionResult CreateCampaign()
        {
            var save = Session.Save(new Campaign
            {
                Account = null, Name = "test"
            });
            return Json(save);
        }

        public ActionResult Add(long id)
        {
            //Session.Save(new Ad
            //{
            //    Campaign = Session.Load<Campaign>(id),
            //    Name = "ad"
            //});
            //Session.Save(new NegativeAd
            //{
            //    Campaign = Session.Load<Campaign>(id),
            //    Name = "NegativeAd",
            //    WhatThatSucks = "because they suck"
            //});
            //Session.Save(new PositiveAd()
            //{
            //    Campaign = Session.Load<Campaign>(id),
            //    Name = "PositiveAd",
            //    WhyThisIsAwesome = "yeah"
            //});
            return Json("ok");
        }

        public ActionResult Show(long id)
        {
            var list = Session.Query<Ad>()
                .Where(x => x.Campaign.Id == id)
                .ToList();
            return Json(list.Select(x => x.Name).ToList());
        }

        //public ActionResult ShowNegative(long id)
        //{
        //    return Json(Session.Load<NegativeAd>(id).Name);
        //}

        public ActionResult ShowThroughModel(long id)
        {
            var list = Session.Get<Campaign>(id).Ads
                .Select(x => x.Name).ToList();

            return Json(list);
        }
    }
}