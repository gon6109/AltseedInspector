using System;

namespace InspectorModel
{
    /// <summary>
    /// ボタン
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class ButtonAttribute : BaseAttribute
    {
        public string Name { get; }

        public ButtonAttribute(string name)
        {
            Name = name;
        }
    }
}
