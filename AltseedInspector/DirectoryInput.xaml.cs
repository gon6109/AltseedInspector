using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;

namespace AltseedInspector
{
    /// <summary>
    /// DirectoryInput.xaml の相互作用ロジック
    /// </summary>
    public partial class DirectoryInput : UserControl
    {
        public object BindingSource { get; }

        /// <summary>
        /// ルートディレクトリ
        /// </summary>
        public string RootPathBinding { get; }

        public DirectoryInput(string itemName, string bindingPath, object bindingSource, string rootPathBinding)
        {
            InitializeComponent();

            var bind = new Binding(bindingPath);
            bind.Source = bindingSource;
            bind.Mode = BindingMode.TwoWay;
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            Path.SetBinding(TextBox.TextProperty, bind);

            ItemName.Content = itemName;
            RootPathBinding = rootPathBinding;

            BindingSource = bindingSource;
        }

        private void Dialog_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog openFileDialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            openFileDialog.InitialDirectory = "";
            openFileDialog.IsFolderPicker = true;
            openFileDialog.RestoreDirectory = true;
            var result = openFileDialog.ShowDialog(new WindowInteropHelper(Window.GetWindow(this)).Handle);
            if (result == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok) Path.Text = openFileDialog.FileName + "\\";
            if (RootPathBinding != null)
            {
                var rootPath = (string)BindingSource.GetType().GetProperties().Cast<PropertyInfo>()
                    .First(obj =>
                        obj.GetCustomAttribute(typeof(InspectorModel.RootPathBindingAttribute)) is InspectorModel.RootPathBindingAttribute bindingAttribute &&
                        bindingAttribute.Name == RootPathBinding)
                    .GetValue(BindingSource);
                Path.Text = InspectorModel.Path.GetRelativePath(Path.Text, RootPathBinding);
            }
        }
    }
}
