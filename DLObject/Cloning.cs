using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    /// <summary>
    /// a class that extend object
    /// </summary>
    static class Cloning
    {
        /// <summary>
        /// the method is cloning the object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject = new T();
            //T copyToObject = (T)Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);

            return copyToObject;
        }
    }
}
