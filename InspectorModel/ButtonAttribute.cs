using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectorModel
{
    /// <summary>
    /// ボタン
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class ButtonAttribute : BaseAttribute
    {
        public string Name { get; private set; }

        public ButtonAttribute(string name)
        {
            Name = name;
        }
    }
}
