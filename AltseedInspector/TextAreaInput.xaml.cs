using System.Windows.Controls;

namespace AltseedInspector
{
    /// <summary>
    /// TextAreaInput.xaml の相互作用ロジック
    /// </summary>
    public partial class TextAreaInput : UserControl
    {
        public TextAreaInput(string itemName, string bindingPath, object bindingSource)
        {
            InitializeComponent();

            ItemName.Content = itemName;
            var bind = new System.Windows.Data.Binding(bindingPath);
            bind.Source = bindingSource;
            bind.Mode = System.Windows.Data.BindingMode.TwoWay;
            bind.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
            Text.SetBinding(TextBox.TextProperty, bind);
        }
    }
}
