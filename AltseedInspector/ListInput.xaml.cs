﻿using InspectorModel;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AltseedInspector
{
    /// <summary>
    /// ListInput.xaml の相互作用ロジック
    /// </summary>
    public partial class ListInput : UserControl
    {
        object BindingSource { get; set; }
        object SelectedObject { get; set; }
        public MethodInfo AddButtonEventMethodInfo { get; }
        public MethodInfo RemoveButtonEventMethodInfo { get; }
        public string SelectedItemBindingPath { get; private set; }

        public ListInput(string groupName, object collection, object bindingSource)
        {
            InitializeComponent();

            BindingSource = bindingSource;
            expander.Header = groupName;

            AddButtonEventMethodInfo = BindingSource.GetType().GetMethods().Cast<MethodInfo>()
                .FirstOrDefault(obj =>
                    obj.GetCustomAttribute(typeof(AddButtonMethodBindingAttribute)) is AddButtonMethodBindingAttribute bindingAttribute &&
                    bindingAttribute.Name == groupName);
            RemoveButtonEventMethodInfo = BindingSource.GetType().GetMethods().Cast<MethodInfo>()
                .FirstOrDefault(obj =>
                    obj.GetCustomAttribute(typeof(RemoveButtonMethodBindingAttribute)) is RemoveButtonMethodBindingAttribute bindingAttribute &&
                    bindingAttribute.Name == groupName);
            if (AddButtonEventMethodInfo == null) button1.Visibility = Visibility.Collapsed;
            if (RemoveButtonEventMethodInfo == null) button.Visibility = Visibility.Collapsed;

            SelectedItemBindingPath = BindingSource.GetType().GetProperties().Cast<PropertyInfo>()
                .FirstOrDefault(obj =>
                    obj.GetCustomAttribute(typeof(SelectedItemBindingAttribute)) is SelectedItemBindingAttribute bindingAttribute &&
                    bindingAttribute.Name == groupName)
                ?.Name;

            DataContext = collection;

            if (BindingSource is INotifyPropertyChanged propertyChanged)
            {
                propertyChanged.PropertyChanged += PropertyChanged_PropertyChanged;
            }

            if (listBox.Items.Count == 1) listBox.SelectedIndex = 0;
            if (SelectedItemBindingPath != null &&
                BindingSource.GetType().GetProperty(SelectedItemBindingPath).GetValue(BindingSource) != null)
            {
                var selected = BindingSource.GetType().GetProperty(SelectedItemBindingPath).GetValue(BindingSource);
                if (((IEnumerable)collection).Cast<IListInput>().Any(obj => obj == selected))
                {
                    listBox.SelectedItem = selected;
                    SelectedObject = selected;
                }
            }
        }

        private void PropertyChanged_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SelectedItemBindingPath)
            {
                var selected = BindingSource.GetType().GetProperty(SelectedItemBindingPath).GetValue(BindingSource);
                if (SelectedObject != selected && ((IEnumerable)DataContext).Cast<IListInput>().Any(obj => obj == selected))
                {
                    listBox.SelectedItem = selected;
                    SelectedObject = selected;
                }
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem.Children.Clear();
            SelectedObject = null;
            if (e.AddedItems.Count != 0)
            {
                var temp = new Property("選択オブジェクト", e.AddedItems[0]);
                SelectedObject = e.AddedItems[0];
                SelectedItem.Children.Add(temp);
            }
            if (SelectedItemBindingPath != null
                && BindingSource.GetType().GetProperty(SelectedItemBindingPath).GetValue(BindingSource) != SelectedObject) BindingSource.GetType().GetProperty(SelectedItemBindingPath).SetValue(BindingSource, SelectedObject);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null) return;
            SelectedItem.Children.Clear();
            RemoveButtonEventMethodInfo?.Invoke(BindingSource, new object[] { SelectedObject });
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddButtonEventMethodInfo?.Invoke(BindingSource, new object[] { });
        }

        /// <summary>
        /// ドラッグデータ形式を表す文字列
        /// <summary>
        private const string DRAG_DATA_FMT = "ItemsControlDragAndDropData";

        /// <summary>
        /// 開始位置
        /// <summary>
        private Point? startPos = null;

        /// <summary>
        /// ドラッグアイテムのインデックス
        /// <summary>
        private int? dragItemIdx = null;

        /// <summary>
        /// ドラッグデータ
        /// <summary>
        private DataObject dragData = null;

        /// <summary>
        /// マウス左ボタン押下ハンドラ
        /// <summary>
        private void listBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //送信者判定
            ItemsControl itemsControl = sender as ItemsControl;
            if (itemsControl != null)
            {
                //ドラッグアイテムを取得
                var dragItem = e.OriginalSource as FrameworkElement;
                if (dragItem != null)
                {
                    //ドラッグアイテムのデータ取得
                    var itemData = GetItemData(itemsControl, dragItem);
                    if (itemData != null)
                    {
                        dragData = new DataObject(DRAG_DATA_FMT, itemData);
                        dragItemIdx = GetItemIndex(itemsControl, itemData);

                        var pos = e.GetPosition(itemsControl);
                        startPos = itemsControl.PointToScreen(pos);
                    }
                }
            }
        }

        /// <summary>
        /// 指定アイテムを所有するコンテナを取得する。
        /// </summary>
        private FrameworkElement GetItemContainer(ItemsControl itemsControl, DependencyObject item)
        {
            //パラメータ確認
            if ((itemsControl == null) || (item == null))
            {
                return null;
            }

            //コンテナを取得
            return itemsControl.ContainerFromElement(item) as FrameworkElement;
        }


        /// <summary>
        /// 指定アイテムのデータを取得する。
        /// </summary>
        private object GetItemData(ItemsControl itemsControl, DependencyObject item)
        {
            var container = GetItemContainer(itemsControl, item);
            return (container == null) ? null : container.DataContext;
        }

        /// <summary>
        /// 指定アイテムデータのItemsControl内でのインデックスを取得する。
        /// </summary>
        private int? GetItemIndex(ItemsControl itemsControl, object data)
        {
            var items = itemsControl.ItemsSource as IList;
            int idx = items.IndexOf(data);
            return (idx != -1) ? idx : (int?)null;
        }

        /// <summary>
        /// ドラッグ＆ドロップ関連データをクリーンアップする。
        /// </summary>
        private void CleanUpDragDropAndDropData()
        {
            startPos = null;
            dragItemIdx = null;
            dragData = null;
        }

        /// <summary>
        /// PreviewMouseMoveハンドラ
        /// </summary>
        private void listBox_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ItemsControl itemsControl = sender as ItemsControl;
            if (itemsControl != null)
            {
                if (startPos != null)
                {
                    //ドラッグ開始可否判定
                    Point curPos = itemsControl.PointToScreen(e.GetPosition(itemsControl));
                    Vector diff = curPos - (Point)startPos;
                    if (IsDragStartable(diff))
                    {
                        DragDrop.DoDragDrop(itemsControl, dragData, DragDropEffects.Move);

                        CleanUpDragDropAndDropData();
                    }
                }
            }
        }

        /// <summary>
        /// ドラッグ開始とする距離を移動したかどうかの判定を行う。
        /// </summary>
        private bool IsDragStartable(Vector delta)
        {
            return (SystemParameters.MinimumHorizontalDragDistance < Math.Abs(delta.X)) ||
                   (SystemParameters.MinimumVerticalDragDistance < Math.Abs(delta.Y));
        }

        /// <summary>
        /// PreviewMouseLeftButtonUpハンドラ
        /// </summary>
        private void listBox_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CleanUpDragDropAndDropData();
        }

        /// <summary>
        /// PreviewDragEnterハンドラ
        /// </summary>
        private void listBox_PreviewDragEnter(object sender, DragEventArgs e)
        {
            bool isDragData = e.Data.GetDataPresent(DRAG_DATA_FMT);
            if (isDragData && (sender == e.Source))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        /// <summary>
        /// PreviewDragOverハンドラ
        /// </summary>
        private void listBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            bool isDragData = e.Data.GetDataPresent(DRAG_DATA_FMT);
            if (isDragData && (sender == e.Source))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        /// <summary>
        /// Dropハンドラ
        /// </summary>
        private void listBox_Drop(object sender, DragEventArgs e)
        {
            var itemsControl = sender as ItemsControl;
            if (dragData == null) return;
            if (itemsControl != null)
            {
                var items = itemsControl.ItemsSource as IList;
                var data = dragData.GetData(DRAG_DATA_FMT);

                var dropObj = e.OriginalSource as DependencyObject;
                var dropData = GetItemData(itemsControl, dropObj);
                int? dropItemIdx = GetItemIndex(itemsControl, dropData);
                if (dropItemIdx != null)
                {
                    items.Remove(data);
                    items.Insert((int)dropItemIdx, data);
                }
                else
                {
                    items.Remove(data);
                    items.Add(data);
                }

                CleanUpDragDropAndDropData();
            }
        }
    }
}
