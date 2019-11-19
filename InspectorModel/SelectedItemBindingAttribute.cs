﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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