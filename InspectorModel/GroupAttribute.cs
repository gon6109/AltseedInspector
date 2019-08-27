using System;

namespace InspectorModel
{
    /// <summary>
    /// グループ化
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class GroupAttribute : BaseAttribute
    {
        // This is a positional argument
        public GroupAttribute(string itemName)
        {
            ItemName = itemName;
        }

        public string ItemName { get; private set; }
    }
}
