using Avalonia;
using Avalonia.Controls;
using System.Windows.Input;

namespace TestAvalonia2.Behaviors
{
    internal class MenuItemBehaviors
    {
        public static readonly AttachedProperty<ICommand> ClickCommandProperty =
            AvaloniaProperty.RegisterAttached<MenuItemBehaviors, MenuItem, ICommand>(
                "ClickCommand");

        static MenuItemBehaviors()
        {
            ClickCommandProperty.Changed.AddClassHandler<MenuItem>(OnClickCommandChanged);
        }

        public static ICommand GetClickCommand(MenuItem element)
            => element.GetValue(ClickCommandProperty);

        public static void SetClickCommand(MenuItem element, ICommand value)
            => element.SetValue(ClickCommandProperty, value);

        private static void OnClickCommandChanged(MenuItem menuItem, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is ICommand)
                menuItem.Click -= OnMenuItemClicked;

            if (e.NewValue is ICommand)
                menuItem.Click += OnMenuItemClicked;
        }

        private static void OnMenuItemClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                var command = GetClickCommand(menuItem);
                if (command?.CanExecute(menuItem.CommandParameter) == true)
                    command.Execute(menuItem.CommandParameter);
            }
        }
    }
}