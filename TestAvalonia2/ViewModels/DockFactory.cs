using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm;
using Dock.Model.Mvvm.Controls;
using System;
using System.Collections.Generic;
using TestAvalonia2.Services;
using TestAvalonia2.ViewModels.Tools;
using TestAvalonia2.ViewModels.Views;

namespace TestAvalonia2.ViewModels
{
    public class DockFactory : Factory
    {
        private readonly IEventAggregator _eventAggregator;
        private IRootDock? _rootDock;
        private IProportionalDock? _documentDock;

        public DockFactory()
        {
            _eventAggregator = new EventAggregator();
        }

        public override IRootDock CreateLayout()
        {
            Document home = new HomeViewModel() { Id = "HomeVM" };
            Tool tool1 = new ToolViewModel() { Id = "T1", CanClose = false, CanPin = false };
            Tool tool2 = new ToolViewModel() { Id = "T2", CanClose = false, CanPin = false };
            Tool tool3 = new ToolViewModel() { Id = "T3", CanClose = false, CanPin = false };

            ToolDock leftToolDock = new ToolDock
            {
                Id = "LeftTools",
                ActiveDockable = tool1,
                VisibleDockables = CreateList<IDockable>(tool1),
                Alignment = Alignment.Left
            };
            ToolDock rightToolDock = new ToolDock
            {
                Id = "RightTools",
                Proportion = 0.25,
                ActiveDockable = tool2,
                VisibleDockables = CreateList<IDockable>(tool2),
                Alignment = Alignment.Right,
            };
            ToolDock bottomToolDock = new ToolDock
            {
                Id = "BottomTools",
                Proportion = 0.25,
                ActiveDockable = tool3,
                VisibleDockables = CreateList<IDockable>(tool3),
                Alignment = Alignment.Bottom
            };
            ProportionalDock HomeDock = new ProportionalDock()
            {
                Id = "Home",
                ActiveDockable = home,
                VisibleDockables = CreateList<IDockable>(home)
            };

            ProportionalDock leftDock = new ProportionalDock
            {
                Id = "LeftDock",
                Proportion = 0.25,
                Orientation = Orientation.Vertical,
                VisibleDockables = CreateList<IDockable>(
                    leftToolDock
                )
            };
            ProportionalDock rightDock = new ProportionalDock
            {
                Id = "RightDock",
                Proportion = 0.25,
                Orientation = Orientation.Vertical,
                VisibleDockables = CreateList<IDockable>(
                    rightToolDock
                )
            };
            ProportionalDock centerDock = new ProportionalDock
            {
                Id = "CenterDock",
                Orientation = Orientation.Vertical,
                VisibleDockables = CreateList<IDockable>(
                    HomeDock,
                    new ProportionalDockSplitter(),
                    bottomToolDock
                )
            };

            ProportionalDock mainLayout = new ProportionalDock
            {
                Id = "MainLayout",
                Orientation = Orientation.Horizontal,
                VisibleDockables = CreateList<IDockable>(
                    leftDock,
                    new ProportionalDockSplitter(),
                    centerDock,
                    new ProportionalDockSplitter(),
                    rightToolDock
                )
            };

            RootDock rootDock = new RootDock
            {
                Id = "Root",
                IsCollapsable = false,
                ActiveDockable = mainLayout,
                DefaultDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            _documentDock = HomeDock;
            _rootDock = rootDock;
            return rootDock;
        }

        public override void InitLayout(IDockable layout)
        {
            ContextLocator = new Dictionary<string, Func<object?>>
            {
                ["HomeVM"] = () => new HomeViewModel(),
                ["T1"] = () => new ToolViewModel(),
                ["T2"] = () => new ToolViewModel(),
                ["T3"] = () => new ToolViewModel()
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