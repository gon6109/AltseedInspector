using System;

namespace InspectorModel
{
    /// <summary>
    /// リストボックス追加ボタンバインディング
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class AddButtonMethodBindingAttribute : BaseAttribute
    {
        public string Name { get; }

        public AddButtonMethodBindingAttribute(string name)
        {
            Name = name;
        }
    }
}
