using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpBoot.Common.Utils
{
    /// <summary>
    /// BeanUtils提供用于拷贝对象中的具体字段的值的方法
    /// </summary>
    public class BeanUtils
    {
        /// <summary>
        /// 把源对象里的各个字段的内容直接赋值给目标对象（只是字段复制，两个对象的字段名和类型都必须一致）
        /// </summary>
        /// <param name="dest">目标对象</param>
        /// <param name="src">源对象</param>
        public static void CopyObject(object src, object dest)
        {
            if (null == src) { return; }
            if (null == dest) { return; }

            Type srcType = src.GetType();
            Type destType = dest.GetType();

            PropertyInfo[] src_ps = srcType.GetProperties();
            PropertyInfo[] des_ps = destType.GetProperties();

            des_ps.ToList().ForEach(a =>
            {
                var src_p = src_ps.Where(b => b.Name == a.Name).FirstOrDefault();
                var setMethod = a.GetSetMethod();
                if (src_p != null && src_p.PropertyType == a.PropertyType)
                {
                    setMethod?.Invoke(dest, new object[] { src_p.GetValue(src) });
                }

            });
        }


        public static TResult Map<TResult>(object src)
        {
            if (src == null) return default(TResult);
            TResult result = Activator.CreateInstance<TResult>();
            CopyObject(src, result);
            return result;
        }


        /// <summary>
        /// 从一个对象里复制属性到另一个对象的同名属性
        /// </summary>
        /// <param name="dest">目标对象</param>
        /// <param name="src">源对象</param>
        /// <param name="fieldName">要复制的属性名字</param>
        public static void CopyProperty(ref object dest, object src, string fieldName)
        {
            if (null == src || null == dest || null == fieldName)
            {
                throw new ArgumentNullException("one argument is null!");
            }
            Type srcType = src.GetType();
            Type destType = dest.GetType();
            FieldInfo srcInfo = srcType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            FieldInfo destInfo = destType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            object val = srcInfo.GetValue(src);
            destInfo.SetValue(dest, val);

        }


        /// <summary>
        /// 用于设置对象的属性值
        /// </summary>
        /// <param name="dest">目标对象</param>
        /// <param name="fieldName">属性名字</param>
        /// <param name="value">属性里要设置的值</param>
        public static void SetProperty(ref object dest, string fieldName, object value)
        {
            if (null == dest || null == fieldName || null == value)
            {
                throw new ArgumentNullException("one argument is null!");
            }
            Type t = dest.GetType();
            FieldInfo f = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            f.SetValue(dest, value);
        }

    }
}
