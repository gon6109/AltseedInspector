using System;

namespace InspectorModel
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class BaseAttribute : System.Attribute
    {
        public BaseAttribute()
        {

        }
    }
}
