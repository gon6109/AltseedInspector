using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// PrimitiveListInput.xaml の相互作用ロジック
    /// </summary>
    public partial class PrimitiveListInput : UserControl
    {

        public PrimitiveListInput(string groupName, IList collection)
        {
            InitializeComponent();

            expander.Header = groupName;

            DataContext = collection;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is IList list)
            {
                list.RemoveAt(listBox.SelectedIndex);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is IList list)
            {
                Type type = list.GetType().GetGenericArguments().FirstOrDefault();
                if (type == typeof(string))
                    list.Add("");
                else
                    list.Add(Activator.CreateInstance(type));
                DataContext = DataContext;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                FrameworkElement element = textBox;
                while (!(element is ListBoxItem))
                {
                    element = (FrameworkElement)element.TemplatedParent;
                }
                
                if (element is ListBoxItem listBoxItem)
                {
                    Selector.SetIsSelected(listBoxItem, true);
                }
            }
        }
    }
}
