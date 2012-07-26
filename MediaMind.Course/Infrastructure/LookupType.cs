using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MediaMind.Course.Models;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using NHibernate.Linq;
using System.Linq;

namespace MediaMind.Course.Infrastructure
{
   
    [Serializable]
    public class FormatTypeLookupType : LookupType<FormatType>
    {
        public FormatTypeLookupType()
        {
            
        }

    }

    [Serializable]
    public class LookupType<T> : IUserType
    {
        private static Task<IDictionary<long, dynamic>> itemsTask;

        public static void Init(ISessionFactory sessionFactory)
        {
            itemsTask = Task.Factory.StartNew(() =>
            {
                using (var session = sessionFactory.OpenSession())
                {
                    IDictionary<long, dynamic> items = new Dictionary<long, dynamic>();

                    var list = session.Query<T>().ToList();
                    foreach (var item in list)
                    {
                        items.Add(((dynamic) item).Id, item);
                    }

                    return items;
                }
            });
        }

        public bool Equals(dynamic x, dynamic y)
        {
            return x == null && y == null ||
                   (x != null && y != null &&
                    x.Id == y.Id);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var o = rs[names[0]];
            if (o == DBNull.Value)
                return null;
            return itemsTask.Result[(long) o].Clone();
        }

        public void NullSafeSet(IDbCommand cmd, dynamic value, int index)
        {
            ((IDataParameter) cmd.Parameters[index]).Value = value.Id;
        }

        public object DeepCopy(dynamic  value)
        {
            return value.Clone();
        }

        public object Replace(object original, object target, object owner)
        {
            return DeepCopy(original);
        }

        public object Assemble(object cached, object owner)
        {
            throw new NotImplementedException();
        }

        public object Disassemble(object value)
        {
            throw new NotImplementedException();
        }

        public SqlType[] SqlTypes
        {
            get { return new[]{NHibernateUtil.Int64.SqlType}; }
        }

        public Type ReturnedType
        {
            get { return typeof(T); }
        }

        public bool IsMutable
        {
            get { return false;
            }
        }
    }
}