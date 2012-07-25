﻿using System.Reflection;
using System.Threading;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Cfg;
using Environment = System.Environment;

namespace MediaMind.Course.Controllers
{
	public abstract class NHibernateController
		: Controller
	{
		private static ISessionFactory sessionFactory;

		public static ISessionFactory SessionFactory
		{
			get
			{
				if(sessionFactory == null)
				{
					lock(typeof(NHibernateController))
					{
						Thread.MemoryBarrier();
						if(sessionFactory == null)
						{
							sessionFactory = CreateSessionFactory();
						}
					}
				}
				return sessionFactory;
			}
		}

		private static ISessionFactory CreateSessionFactory()
		{
			var cfg = new Configuration();
			cfg.DataBaseIntegration(properties =>
			{
				properties.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
				properties.ConnectionStringName = Environment.MachineName;
			});
			cfg.AddAssembly(Assembly.GetExecutingAssembly());
			return cfg.BuildSessionFactory();
		}


		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
		}
	}
}