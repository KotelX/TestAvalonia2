// TreeViewBehaviors.cs - пишется один раз
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System.Windows.Input;

namespace TestAvalonia2.Behaviors
{
    public static class TreeViewBehaviors
    {
        public static readonly AttachedProperty<ICommand> DoubleClickCommandProperty =
            AvaloniaProperty.RegisterAttached<TreeView, ICommand>(
                "DoubleClickCommand",
                typeof(TreeViewBehaviors));

        static TreeViewBehaviors()
        {
            DoubleClickCommandProperty.Changed.AddClassHandler<TreeView>(OnDoubleClickCommandChanged);
        }

        public static ICommand GetDoubleClickCommand(TreeView element)
            => element.GetValue(DoubleClickCommandProperty);

        public static void SetDoubleClickCommand(TreeView element, ICommand value)
            => element.SetValue(DoubleClickCommandProperty, value);

        private static void OnDoubleClickCommandChanged(TreeView treeView, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is ICommand)
                treeView.DoubleTapped -= OnTreeViewDoubleTapped;

            if (e.NewValue is ICommand)
                treeView.DoubleTapped += OnTreeViewDoubleTapped;
        }

        private static void OnTreeViewDoubleTapped(object? sender, TappedEventArgs e)
        {
            if (sender is TreeView treeView)
            {
                var command = GetDoubleClickCommand(treeView);
                if (command?.CanExecute(treeView.SelectedItem) == true)
                    command.Execute(treeView.SelectedItem);
            }
        }
    }
}