using System;

namespace InspectorModel
{
    /// <summary>
    /// 整数入力
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NumberInputAttribute : BaseAttribute
    {
        public string ItemName { get; set; }

        public NumberInputAttribute(string itemName)
        {
            ItemName = itemName;
        }
    }
}
