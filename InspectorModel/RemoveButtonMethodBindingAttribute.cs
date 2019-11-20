using System;

namespace InspectorModel
{
    /// <summary>
    /// リストボックス消去ボタンバインディング
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class RemoveButtonMethodBindingAttribute : BaseAttribute
    {
        public string Name { get; }

        public RemoveButtonMethodBindingAttribute(string name)
        {
            Name = name;
        }
    }
}
