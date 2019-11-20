using System.Windows.Controls;

namespace AltseedInspector
{
    /// <summary>
    /// TextOutput.xaml の相互作用ロジック
    /// </summary>
    public partial class TextOutput : UserControl
    {
        public TextOutput(string itemName, string bindingPath, object bindingSource)
        {
            InitializeComponent();

            var bind = new System.Windows.Data.Binding(bindingPath);
            bind.Source = bindingSource;
            bind.Mode = System.Windows.Data.BindingMode.OneWay;
            bind.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
            Output.SetBinding(Label.ContentProperty, bind);

            ItemName.Content = itemName;
        }
    }
}
