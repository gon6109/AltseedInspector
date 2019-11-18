using System;

namespace InspectorModel
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class ListInputAttribute : BaseAttribute
    {
        public ListInputAttribute(string groupName, string selectedObjectBindingPath = "", string addButtonEventMethodName = "", string removeButtonEventMethodName = "")
        {
            GroupName = groupName;
            AddButtonEventMethodName = addButtonEventMethodName;
            RemoveButtonEventMethodName = removeButtonEventMethodName;
            SelectedObjectBindingPath = selectedObjectBindingPath;
        }

        public string GroupName { get; set; }
        public string AddButtonEventMethodName { get; set; }
        public string RemoveButtonEventMethodName { get; set; }
        public string SelectedObjectBindingPath { get; set; }
    }

    public interface IListInput
    {
        string Name { get; }
    }
}
