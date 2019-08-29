﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectorModel
{
    /// <summary>
    /// ディレクトリ入力
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class DirectoryInputAttribute : BaseAttribute
    {
        public DirectoryInputAttribute(string itemName, bool isAutoConvertRelativePath = true)
        {
            ItemName = itemName;
            IsAutoConvertRelativePath = isAutoConvertRelativePath;
        }

        public bool IsAutoConvertRelativePath { get; }

        public string ItemName { get; }
    }
}