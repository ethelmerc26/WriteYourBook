using System.Windows;
using System.Windows.Controls;

namespace Book.Helpers
{
    public class ExtendedTreeView : TreeView
    {
        public ExtendedTreeView() : base()
        {
            SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(ChangeSelectedItem);
        }

        void ChangeSelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null)
                SetValue(_SelectedItemProperty, SelectedItem);
        }

        public object SelectedItem_
        {
            get { return (object)GetValue(_SelectedItemProperty); }
            set { SetValue(_SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty _SelectedItemProperty = DependencyProperty.Register("SelectedItem_", typeof(object), typeof(ExtendedTreeView), new UIPropertyMetadata(null));
    }
}