using System.Reflection;
using System.Threading;
using System.Web.Mvc;
using MediaMind.Course.Infrastructure;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
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
				if (sessionFactory == null)
				{
					lock (typeof(NHibernateController))
					{
						Thread.MemoryBarrier();
						if (sessionFactory == null)
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
            //cfg.SetNamingStrategy(new NamingConvention());
            cfg.DataBaseIntegration(properties =>
			{
                //properties.SchemaAction = SchemaAutoAction.Validate;
				properties.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
				properties.ConnectionStringName = Environment.MachineName;
			});
			cfg.AddAssembly(Assembly.GetExecutingAssembly());
            cfg.SetInterceptor(new DontMakeMeCRY());
			var validatingEventListener = new ValidatingEventListener();
			cfg.SetListener(ListenerType.PreInsert, validatingEventListener);
			cfg.SetListener(ListenerType.PreUpdate, validatingEventListener);

            cfg.SetProperty(
                NHibernate.Cfg.Environment.DefaultBatchFetchSize,
                "25");

			return cfg.BuildSessionFactory();
		}


		protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
		}

		public new ISession Session { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Session = SessionFactory.OpenSession();
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			using (Session)
			{
				if (filterContext.Exception != null)
					return;

				if (!Session.IsDirty())
					return;

				using (Session.BeginTransaction())
				{
					Session.Transaction.Commit();
				}
			}
		}

	}
}