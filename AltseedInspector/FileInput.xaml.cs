﻿using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AltseedInspector
{
    /// <summary>
    /// FileInput.xaml の相互作用ロジック
    /// </summary>
    public partial class FileInput : UserControl
    {
        public object BindingSource { get; }
        public string Filter { get; }
        public string RootPathBinding { get; }

        public FileInput(string itemName, string bindingPath, object bindingSource, string filter = "All File|*.*", string rootPathBinding = "")
        {
            InitializeComponent();

            var bind = new System.Windows.Data.Binding(bindingPath);
            bind.Source = bindingSource;
            bind.Mode = System.Windows.Data.BindingMode.TwoWay;
            bind.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
            Path.SetBinding(TextBox.TextProperty, bind);

            BindingSource = bindingSource;
            ItemName.Content = itemName;
            Filter = filter;
            RootPathBinding = rootPathBinding;
        }

        private string RootPath => (string)BindingSource.GetType().GetProperties().Cast<PropertyInfo>()
                        .First(obj =>
                            obj.GetCustomAttribute(typeof(InspectorModel.RootPathBindingAttribute)) is InspectorModel.RootPathBindingAttribute bindingAttribute &&
                            bindingAttribute.Name == RootPathBinding)
                        .GetValue(BindingSource);

        private void Dialog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.FileName = "";
            openFileDialog.Filter = Filter;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (RootPathBinding != null && RootPath != null)
                {
                    Path.Text = InspectorModel.Path.GetRelativePath(openFileDialog.FileName, RootPath);
                }
                else
                    Path.Text = openFileDialog.FileName;
            }
        }
    }
}
