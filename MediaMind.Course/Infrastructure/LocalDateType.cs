using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace MediaMind.Course.Infrastructure
{
    [Serializable]
    public class LocalDateType : IUserType
    {
        public bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var val = rs[names[0]];

            if (val == DBNull.Value)
                return null;

            return ((DateTime)val).ToLocalTime();
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                ((IDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            }
            else
            {
                ((IDataParameter)cmd.Parameters[index]).Value = ((DateTime)value).ToUniversalTime();
            }
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public SqlType[] SqlTypes
        {
            get
            {
                return new[]
                {
                    NHibernateUtil.Date.SqlType
                };
            }
        }

        public Type ReturnedType
        {
            get { return typeof(DateTime); }
        }

        public bool IsMutable
        {
            get { return false; }
        }
    }
}