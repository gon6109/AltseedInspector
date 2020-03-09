using System;

namespace InspectorModel
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class PrimitiveListInputAttribute : BaseAttribute
    {
        public PrimitiveListInputAttribute(string groupName)
        {
            GroupName = groupName;
        }

        public string GroupName { get; set; }
    }
}
