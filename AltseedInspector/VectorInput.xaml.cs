﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace AltseedInspector
{
    /// <summary>
    /// VectorInput.xaml の相互作用ロジック
    /// </summary>
    public partial class VectorInput : UserControl
    {
        public static readonly DependencyProperty VectorProperty = DependencyProperty.Register
          ("Vector", typeof(asd.Vector2DF), typeof(VectorInput), new FrameworkPropertyMetadata(new asd.Vector2DF(), VectorPropertyChanged));

        private static void VectorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vectorInput = d as VectorInput;
            if (vectorInput != null && e.Property == VectorProperty)
            {
                vectorInput.XInput.Text = vectorInput.Vector.X.ToString();
                vectorInput.YInput.Text = vectorInput.Vector.Y.ToString();
            }
        }

        public asd.Vector2DF Vector
        {
            get => (asd.Vector2DF)GetValue(VectorProperty);
            set => SetValue(VectorProperty, value);
        }

        public VectorInput(string itemName, string bindingPath, object bindingSource)
        {
            InitializeComponent();
            var bind = new Binding(bindingPath);
            bind.Source = bindingSource;
            bind.Mode = BindingMode.TwoWay;
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SetBinding(VectorInput.VectorProperty, bind);

            ItemName.Content = itemName;
        }

        private void XInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Vector.X == Convert.ToSingle(XInput.Text)) return;
                Vector = new asd.Vector2DF(Convert.ToSingle(XInput.Text), Vector.Y);
                XInput.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            catch
            {
                XInput.Background = new SolidColorBrush(Color.FromRgb(255, 111, 111));
            }
        }

        private void YInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Vector.Y == Convert.ToSingle(YInput.Text)) return;
                Vector = new asd.Vector2DF(Vector.X, Convert.ToSingle(YInput.Text));
                YInput.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
            catch
            {
                YInput.Background = new SolidColorBrush(Color.FromRgb(255, 111, 111));
            }
        }

        private void XInput_Unloaded(object sender, RoutedEventArgs e)
        {
            BindingOperations.ClearBinding(this, VectorProperty);
        }
    }
}
