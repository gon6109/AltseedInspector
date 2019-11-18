using System;

namespace InspectorModel
{
    /// <summary>
    /// ファイル入力
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class FileInputAttribute : BaseAttribute
    {
        public FileInputAttribute(string itemName, string filter = "All File|*.*", bool isAutoConvertRelativePath = true, string root = null)
        {
            ItemName = itemName;
            Filter = filter;
            IsAutoConvertRelativePath = isAutoConvertRelativePath;
            Root = root;
        }

        public bool IsAutoConvertRelativePath { get; }

        public string ItemName { get; }

        public string Filter { get; }

        public string Root { get; }
    }
}
