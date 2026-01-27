using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Dock.Model.Core;
using StaticViewLocator;
using System;
using System.Diagnostics.CodeAnalysis;

namespace TestAvalonia2
{
    /// <summary>
    /// Given a view model, returns the corresponding view if possible.
    /// </summary>
    [StaticViewLocator]
    [RequiresUnreferencedCode(
        "Default implementation of ViewLocator involves reflection which may be trimmed away.",
        Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]
    public partial class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            if (param is null)
                return null;

            string? name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            Type? type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data)
        {
            if (data is null)
            {
                return false;
            }

            Type? type = data.GetType();
            return data is IDockable || s_views.ContainsKey(type);
        }
    }
}

