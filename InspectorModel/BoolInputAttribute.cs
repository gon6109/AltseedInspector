using System;

namespace InspectorModel
{
    /// <summary>
    /// 真偽値入力
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BoolInputAttribute : BaseAttribute
    {
        public string ItemName { get; set; }

        public BoolInputAttribute(string itemName)
        {
            ItemName = itemName;
        }
    }
}
