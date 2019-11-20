using System;
using System.Windows;
using System.Windows.Controls;

namespace AltseedInspector
{
    /// <summary>
    /// NumberInput.xaml の相互作用ロジック
    /// </summary>
    public partial class NumberInput : UserControl
    {
        public NumberInput(string itemName, string bindingPath, object bindingSource)
        {
            InitializeComponent();
            ItemName.Content = itemName;
            var bind = new System.Windows.Data.Binding(bindingPath);
            bind.Source = bindingSource;
            bind.Mode = System.Windows.Data.BindingMode.TwoWay;
            bind.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
            Number.SetBinding(TextBox.TextProperty, bind);
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            Number.Text = (Convert.ToInt32(Number.Text) + 1).ToString();
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            Number.Text = (Convert.ToInt32(Number.Text) - 1).ToString();
        }
    }
}
