using System.Windows.Controls;
using System.Windows.Data;

namespace AltseedInspector
{
    /// <summary>
    /// BoolInput.xaml の相互作用ロジック
    /// </summary>
    public partial class BoolInput : UserControl
    {
        public BoolInput(string itemName, string bindingPath, object bindingSource)
        {
            InitializeComponent();
            checkBox.Content = itemName;
            var bind = new Binding(bindingPath);
            bind.Source = bindingSource;
            bind.Mode = BindingMode.TwoWay;
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            checkBox.SetBinding(CheckBox.IsCheckedProperty, bind);
        }
    }
}
