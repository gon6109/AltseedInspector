using InspectorModel;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AltseedInspector
{
    /// <summary>
    /// Property.xaml の相互作用ロジック
    /// </summary>
    public partial class Property : UserControl
    {
        /// <summary>
        /// 単一バインディングソースからプロパティを作成
        /// </summary>
        /// <param name="title">ヘッダータイトル</param>
        /// <param name="bindingSource">バインディングソース</param>
        public Property(string title, object bindingSource)
        {
            InitializeComponent();

            expander.Header = title;
            foreach (var item in bindingSource.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static))
            {
                CreateControl(item, bindingSource);
            }
        }

        public Property(string title, object[] bindingSources)
        {
            InitializeComponent();

            expander.Header = title;
            foreach (var item in bindingSources)
            {
                foreach (var item2 in item.GetType().GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static))
                {
                    CreateControl(item2, item);
                }
            }
        }

        /// <summary>
        /// コントロール作成
        /// </summary>
        /// <param name="info">メンバ情報</param>
        /// <param name="bindingSource">メンバを持つバインディングソース</param>
        void CreateControl(MemberInfo info, object bindingSource)
        {
            if (info is PropertyInfo propertyInfo) CreatePropertyControl(propertyInfo, bindingSource);
            else if (info is MethodInfo methodInfo) CreateMethodControl(methodInfo, bindingSource);
        }

        void CreatePropertyControl(PropertyInfo info, object bindingSource)
        {
            Attribute[] controls = Attribute.GetCustomAttributes(info, typeof(BaseAttribute));
            foreach (Attribute att in controls)
            {
                switch (att)
                {
                    case TextOutputAttribute textOutput:
                        PropertyItems.Children.Add(new TextOutput(textOutput.ItemName, info.Name, bindingSource));
                        break;
                    case DirectoryInputAttribute directoryInput:
                        PropertyItems.Children.Add(new DirectoryInput(directoryInput.ItemName, info.Name, bindingSource, directoryInput.RootPathBinding));
                        break;
                    case FileInputAttribute fileInput:
                        PropertyItems.Children.Add(new FileInput(fileInput.ItemName, info.Name, bindingSource, fileInput.Filter, fileInput.RootPathBinding));
                        break;
                    case VectorInputAttribute vectorInput:
                        PropertyItems.Children.Add(new VectorInput(vectorInput.ItemName, info.Name, bindingSource));
                        break;
                    case BoolInputAttribute boolInput:
                        PropertyItems.Children.Add(new BoolInput(boolInput.ItemName, info.Name, bindingSource));
                        break;
                    case NumberInputAttribute numberInput:
                        PropertyItems.Children.Add(new NumberInput(numberInput.ItemName, info.Name, bindingSource));
                        break;
                    case TextInputAttribute textInput:
                        PropertyItems.Children.Add(new TextInput(textInput.ItemName, info.Name, bindingSource, textInput.IsPropertyChanged));
                        break;
                    case TextAreaInputAttribute textAreaInput:
                        PropertyItems.Children.Add(new TextAreaInput(textAreaInput.ItemName, info.Name, bindingSource));
                        break;
                    case GroupAttribute groupAttribute:
                        PropertyItems.Children.Add(new Property(groupAttribute.ItemName, info.GetValue(bindingSource)));
                        break;
                    case ListInputAttribute listInput:
                        if (info.PropertyType.GetGenericArguments()[0] != null && typeof(IListInput).IsAssignableFrom(info.PropertyType.GetGenericArguments()[0]))
                            PropertyItems.Children.Add(
                                new ListInput(listInput.GroupName, info.GetValue(bindingSource), bindingSource));
                        break;
                }
            }
        }

        void CreateMethodControl(MethodInfo info, object bindingSource)
        {
            Attribute[] controls = Attribute.GetCustomAttributes(info, typeof(BaseAttribute));
            foreach (Attribute att in controls)
            {
                ButtonAttribute buttonAttribute = att as ButtonAttribute;
                if (buttonAttribute != null)
                {
                    var temp = new Button();
                    temp.Padding = new Thickness(4, 2, 4, 2);
                    temp.HorizontalAlignment = HorizontalAlignment.Stretch;
                    temp.VerticalAlignment = VerticalAlignment.Center;
                    temp.Content = buttonAttribute.Name;
                    temp.Click += (object sender, RoutedEventArgs e) =>
                    {
                        info.Invoke(bindingSource, null);
                    };
                    PropertyItems.Children.Add(temp);
                    return;
                }
            }
        }
    }
}
