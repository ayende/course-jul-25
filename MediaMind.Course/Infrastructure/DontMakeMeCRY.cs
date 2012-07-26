using System;
using System.Media;
using System.Web;
using NHibernate;

namespace MediaMind.Course.Infrastructure
{
    [Serializable]
	public class DontMakeMeCRY
		: EmptyInterceptor
	{
		public static int StatementCountPerRequest
		{
			get { return (int) (HttpContext.Current.Items["sql.in.req.count"] ?? 0); }
			set { HttpContext.Current.Items["sql.in.req.count"] = value; }
		}

		public override NHibernate.SqlCommand.SqlString 
			OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
		{
			if (++StatementCountPerRequest > 10)
			{
				new SoundPlayer("http://static1.grsites.com/archive/sounds/people/people076.wav")
					.PlaySync();
			}
		    return Optimizations.Apply(sql);
			return base.OnPrepareStatement(sql);
		}
	}
}