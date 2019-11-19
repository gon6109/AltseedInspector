using System;

namespace InspectorModel
{
    /// <summary>
    /// ファイル入力
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class FileInputAttribute : BaseAttribute
    {
        public FileInputAttribute(string itemName, string filter = "All File|*.*", string rootPathBinding = null)
        {
            ItemName = itemName;
            Filter = filter;
            RootPathBinding = rootPathBinding;
        }

        public string ItemName { get; }

        public string Filter { get; }

        public string RootPathBinding { get; }
    }
}
