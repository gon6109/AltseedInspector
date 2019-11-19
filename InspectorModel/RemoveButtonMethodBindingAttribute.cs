using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
