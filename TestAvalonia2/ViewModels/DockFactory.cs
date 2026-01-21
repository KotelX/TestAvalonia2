using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm;
using Dock.Model.Mvvm.Controls;
using System;
using System.Collections.Generic;
using TestAvalonia2.Models.Documents;
using TestAvalonia2.Models.Tools;
using TestAvalonia2.ViewModels.Docks;
using TestAvalonia2.ViewModels.Documents;
using TestAvalonia2.ViewModels.Tools;
using TestAvalonia2.ViewModels.Views;

namespace TestAvalonia2.ViewModels
{
    public class DockFactory : Factory
    {
        private IRootDock? _rootDock;
        private IDocumentDock? _documentDock;
        public DockFactory()
        {
        }

        public override IRootDock CreateLayout()
        {
            var rootDock = CreateRootDock();
            var document1 = new DocumentViewModel() { Id = "D1", Title = "Document1"};
            var document2 = new DocumentViewModel() { Id = "D2", Title = "Document2" };
            var tool1 = new Tool1ViewModel() { Id = "T1", Title = "Tool1" };
            var tool2 = new Tool1ViewModel() { Id = "T2", Title = "Tool2" };
            var leftDock = new ProportionalDock
            {
                Proportion = 0.25,
                Orientation = Orientation.Vertical,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
            (
                new ToolDock
                {
                    ActiveDockable = tool1,
                    VisibleDockables = CreateList<IDockable>(tool1, tool2),
                    Alignment = Alignment.Left,
                    // CanDrop = false
                },
                new ProportionalDockSplitter { CanResize = true, ResizePreview = true },
                new ToolDock
                {
                    ActiveDockable = tool1,
                    VisibleDockables = CreateList<IDockable>(tool1, tool2),
                    Alignment = Alignment.Bottom,
                    CanDrag = false,
                    CanDrop = false
                }
            )
            };
            var documentDock = new CustomDocumentDock
            {
                // DockGroup = "CustomDocumentDock",
                IsCollapsable = false,
                ActiveDockable = document1,
                VisibleDockables = CreateList<IDockable>(document1, document2),
                CanCreateDocument = true,
                // CanDrop = false,
                EnableWindowDrag = true,
                // CanCloseLastDockable = false,
            };
            var mainLayout = new ProportionalDock
            {
                // EnableGlobalDocking = false,
                Orientation = Orientation.Horizontal,
                VisibleDockables = CreateList<IDockable>
            (
                leftDock,
                new ProportionalDockSplitter { ResizePreview = true },
                documentDock
            )
            };
            var dashboardView = new DashboardViewModel
            {
                Id = "Dashboard",
                Title = "Dashboard"
            };
            var homeView = new HomeViewModel
            {
                Id = "Home",
                Title = "Home",
                ActiveDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            rootDock.IsCollapsable = false;
            rootDock.ActiveDockable = dashboardView;
            rootDock.DefaultDockable = homeView;
            rootDock.VisibleDockables = CreateList<IDockable>(dashboardView, homeView);

            rootDock.LeftPinnedDockables = CreateList<IDockable>();

            _documentDock = documentDock;
            _rootDock = rootDock;

            return rootDock;
        }
        public override void InitLayout(IDockable layout)
        {
            ContextLocator = new Dictionary<string, Func<object?>>
            {
                ["Document1"] = () => new DemoDocument(),
                ["Document2"] = () => new DemoDocument(),
                ["Tool1"] = () => new DemoTool(),
                ["Tool2"] = () => new DemoTool(),
                ["Dashboard"] = () => layout
            };

            DockableLocator = new Dictionary<string, Func<IDockable?>>()
            {
                ["Root"] = () => _rootDock,
                ["Documents"] = () => _documentDock
            };

            HostWindowLocator = new Dictionary<string, Func<IHostWindow?>>
            {
                [nameof(IDockWindow)] = () => new HostWindow()
            };

            base.InitLayout(layout);
        }
    }
}
