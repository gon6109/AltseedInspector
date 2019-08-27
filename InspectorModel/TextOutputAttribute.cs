using System;

namespace InspectorModel
{
    /// <summary>
    /// テキスト表示プロパティ
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class TextOutputAttribute : BaseAttribute
    {
        public string ItemName { get; private set; }

        public TextOutputAttribute(string itemName)
        {
            ItemName = itemName;
        }
    }
}
