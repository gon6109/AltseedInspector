using System;

namespace InspectorModel
{
    /// <summary>
    /// リストボックス選択アイテムバインディング
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class SelectedItemBindingAttribute : BaseAttribute
    {
        public string Name { get; }

        public SelectedItemBindingAttribute(string name)
        {
            Name = name;
        }
    }
}
