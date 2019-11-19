using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
