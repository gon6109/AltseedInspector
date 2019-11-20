using System;

namespace InspectorModel
{
    /// <summary>
    /// ルートパス指定
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class RootPathBindingAttribute : BaseAttribute
    {
        public string Name { get; }

        public RootPathBindingAttribute(string name)
        {
            Name = name;
        }
    }
}
