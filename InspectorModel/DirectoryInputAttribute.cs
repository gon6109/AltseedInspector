using System;

namespace InspectorModel
{
    /// <summary>
    /// ディレクトリ入力
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class DirectoryInputAttribute : BaseAttribute
    {
        public DirectoryInputAttribute(string itemName, string rootPathBinding = null)
        {
            ItemName = itemName;
            RootPathBinding = rootPathBinding;
        }

        public string ItemName { get; }

        public string RootPathBinding { get; }
    }
}
