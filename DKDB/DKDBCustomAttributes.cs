﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DKDB
{
    public class DKDBCustomAttributes
    {
        public static Attribute GetAttribute(Type t, PropertyInfo info)
        {
            return info.GetCustomAttribute(t);
        }

        public static int GetLength(PropertyInfo info)
        {
            DKDBMaxLengthAttribute attr = (DKDBMaxLengthAttribute)info.GetCustomAttribute(typeof(DKDBMaxLengthAttribute));
            return attr.MaxLength;
        }
        

        public static string GetOTMTarget(PropertyInfo info)
        {
            OneToMany attr = (OneToMany)info.GetCustomAttribute(typeof(OneToMany));
            if (attr == null) return null;
            return attr.Target;
        }

        public static Tuple<String,String> GetMTMTargetAndTable(PropertyInfo info)
        {
            ManyToMany attr = (ManyToMany)info.GetCustomAttribute(typeof(ManyToMany));
            if (attr == null) return null;
            return new Tuple<string, string>(attr.TableName, attr.Target);
        }

        public static bool Validator(object o)
        {
            PropertyInfo[] infos = o.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (!info.PropertyType.IsGenericType)
                {
                    DKDBMaxLengthAttribute attr = (DKDBMaxLengthAttribute)info.GetCustomAttribute(typeof(DKDBMaxLengthAttribute));

                    Type t = info.PropertyType;
                    if (attr != null && attr.MaxLength < ((String)(info.GetValue(o))).Length)
                    {
                        return false;
                    }
                }
                
            }
            return true;
        }

        /// <summary>
        /// max Length attribute for String properties to be validated by DKDB
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class DKDBMaxLengthAttribute : Attribute
        {
            public int MaxLength { get; set; }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class OneToMany: Attribute
        {
            public String Target { get; set; }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class ManyToMany: Attribute
        {
            public String Target { get; set; }
            public String TableName { get; set; }
        }

    }
}