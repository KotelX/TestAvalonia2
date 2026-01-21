using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm;
using Dock.Model.Mvvm.Controls;
using SkiaSharp;
using System;
using System.Collections.Generic;
using TestAvalonia2.Models.Documents;
using TestAvalonia2.ViewModels.Docks;
using TestAvalonia2.ViewModels.Documents;
using TestAvalonia2.ViewModels.Tools;

namespace TestAvalonia2.ViewModels
{
    public class DockFactory : Factory
    {
        private IRootDock? _rootDock;
        private IDocumentDock? _documentDock;

        public override IRootDock CreateLayout()
        {
            var document1 = new DocumentViewModel() { Id = "D1", Title = "Document1" };
            var document2 = new DocumentViewModel() { Id = "D2", Title = "Document2" };
            var document3 = new DocumentViewModel() { Id = "D3", Title = "Document3" };
            var tool1 = new ToolViewModel() { Id = "T1", Title = "Tool1" };
            var tool2 = new ToolViewModel() { Id = "T2", Title = "Tool2" };
            var tool3 = new ToolViewModel() { Id = "T3", Title = "Tool3" };
            var tool4 = new ToolViewModel() { Id = "T4", Title = "Tool4" };
            var documentDock = new CustomDocumentDock
            {
                Title = "Doc",
                IsCollapsable = false,
                ActiveDockable = document1,
                VisibleDockables = CreateList<IDockable>(document1, document2, document3),
                CanCreateDocument = true,
                EnableWindowDrag = true
            };

            var leftTopToolDock = new ToolDock
            {
                Id = "LeftTopTools",
                Title = "Верх",
                ActiveDockable = tool1,
                VisibleDockables = CreateList<IDockable>(tool1),
                Alignment = Alignment.Left
            };

            var leftBottomToolDock = new ToolDock
            {
                Id = "LeftBottomTools",
                Title = "Низ",
                ActiveDockable = tool2,
                VisibleDockables = CreateList<IDockable>(tool2),
                Alignment = Alignment.Left
            };

            var leftDock = new ProportionalDock
            {
                Id = "LeftDock",
                Proportion = 0.25,
                Orientation = Orientation.Vertical,
                VisibleDockables = CreateList<IDockable>(
                    leftTopToolDock,
                    new ProportionalDockSplitter(),
                    leftBottomToolDock
                )
            };

            var bottomToolDock = new ToolDock
            {
                Id = "BottomTools",
                Title = "Низ",
                Proportion = 0.25,
                ActiveDockable = tool3,
                VisibleDockables = CreateList<IDockable>(tool3, tool4),
                Alignment = Alignment.Bottom
            };

            var centerDock = new ProportionalDock
            {
                Id = "CenterDock",
                Orientation = Orientation.Vertical,
                VisibleDockables = CreateList<IDockable>(
                    documentDock,
                    new ProportionalDockSplitter(),
                    bottomToolDock
                )
            };

            var mainLayout = new ProportionalDock
            {
                Id = "MainLayout",
                Orientation = Orientation.Horizontal,
                VisibleDockables = CreateList<IDockable>(
                    leftDock,
                    new ProportionalDockSplitter(),
                    centerDock
                )
            };

            var rootDock = new RootDock
            {
                Id = "Root",
                Title = "Root",
                IsCollapsable = false,
                ActiveDockable = mainLayout,
                DefaultDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

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
                ["Document3"] = () => new DemoDocument(),
                ["Tool1"] = () => new Tool(),
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