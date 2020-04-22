using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Dal
{
    static class Cloning
    {
        internal static T Clone<T>(this T source)
        {
            //create instance of our type using reflection
            T copy = (T)Activator.CreateInstance(source.GetType());
            var Info = source.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var i in Info)
            {
                //if it is value type or string that act like value type
                if (i.FieldType.IsValueType || i.FieldType.Equals(typeof(string)))
                {
                    i.SetValue(copy, i.GetValue(source));
                }
                //if is refernce type or Array  types 
                else
                {
                    if (i.FieldType.IsArray && i.FieldType.GetElementType().IsValueType)
                    {

                        Array array = (Array)i.GetValue(source);
                        i.SetValue(copy, array.Clone());
                    }
                    else
                    {

                        if (i.GetValue(source) == null)//initialize to null if it is null
                        {
                            i.SetValue(copy, null);
                        }
                        else
                        {
                            i.SetValue(copy, i.GetValue(source).Clone());//sorce.clone
                        }

                    }
                }
            }
            return copy;
        }
    }
}
