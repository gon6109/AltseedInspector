using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AltseedInspector
{
    /// <summary>
    /// Inspector.xaml の相互作用ロジック
    /// </summary>
    public partial class Inspector : UserControl
    {
        public Inspector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// プロパティを追加する
        /// </summary>
        /// <param name="property">プロパティ</param>
        public void AddProperty(Property property)
        {
            Properties.Children.Add(property);
            Focus();
        }

        /// <summary>
        /// プロパティを消去する
        /// </summary>
        public void ClearProperty()
        {
            Properties.Children.Clear();
        }

        /// <summary>
        /// 条件を満たすのプロパティを消去する
        /// </summary>
        /// <param name="func">条件</param>
        public void RemoveProperty(Func<UIElement, bool> func)
        {
            foreach (var item in Properties.Children.Cast<UIElement>().Where(obj => obj is Property && func(obj)).ToList())
            {
                Properties.Children.Remove(item);
            }
        }
    }
}
