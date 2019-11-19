using System;

namespace InspectorModel
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class ListInputAttribute : BaseAttribute
    {
        public ListInputAttribute(string groupName)
        {
            GroupName = groupName;
        }

        public string GroupName { get; set; }
    }

    public interface IListInput
    {
        string Name { get; }
    }
}
