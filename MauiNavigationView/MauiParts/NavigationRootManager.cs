﻿using System;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Microsoft.Maui.Platform
{
	public partial class NavigationRootManager
	{
		Window _platformWindow;
		WindowRootView _rootView;
		bool _disconnected = true;
		internal event EventHandler? OnApplyTemplateFinished;

		public NavigationRootManager(Window platformWindow)
		{
			_platformWindow = platformWindow;
			_rootView = new WindowRootView();
			_rootView.BackRequested += OnBackRequested;

			// https://learn.microsoft.com/en-us/windows/apps/design/basics/titlebar-design
			// Standard title bar height is 32px
			// This should always get set by the code after but
			// we are setting it just in case
			var appbarHeight = 0;
			//if (AppWindowTitleBar.IsCustomizationSupported())
			//{
			//	var density = _platformWindow.GetDisplayDensity();
			//	appbarHeight = (int)(_platformWindow.AppWindow.TitleBar.Height / density);
			//}

			_rootView.UpdateAppTitleBar(
					appbarHeight,
					AppWindowTitleBar.IsCustomizationSupported() &&
					_platformWindow.AppWindow.TitleBar.ExtendsContentIntoTitleBar
				);

			_rootView.OnApplyTemplateFinished += WindowRootViewOnApplyTemplateFinished;
		}

		void WindowRootViewOnWindowTitleBarContentSizeChanged(object? sender, EventArgs e)
		{
			if (_disconnected)
				return;

			//_platformWindow?
			//	.GetWindow()?
			//	.Handler?
			//	.UpdateValue(nameof(IWindow.TitleBarDragRectangles));
		}

		void WindowRootViewOnApplyTemplateFinished(object? sender, System.EventArgs e) =>
			OnApplyTemplateFinished?.Invoke(this, EventArgs.Empty);

		void OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
		{
			_platformWindow
				.GetWindow()?
				.BackButtonClicked();
		}

		internal FrameworkElement? AppTitleBar => _rootView.AppTitleBar;
		internal MauiToolbar? Toolbar => _rootView.Toolbar;
		public FrameworkElement RootView => _rootView;

		public virtual void Connect(UIElement platformView)
		{
			if (_rootView.Content != null)
			{
				// Clear out the toolbar that was set from the previous content
				SetToolbar(null);

				// We need to make sure to clear out the root view content 
				// before creating the new view.
				// Otherwise the new view might try to act on the old content.
				// It might have code in the handler that retrieves this class.
				_rootView.Content = null;
			}

			NavigationView rootNavigationView;
			if (platformView is NavigationView nv)
			{
				rootNavigationView = nv;
				_rootView.Content = platformView;
			}
			else
			{
				if (_rootView.Content is RootNavigationView navView)
				{
					rootNavigationView = navView;
				}
				else
				{
					rootNavigationView = new RootNavigationView();
				}

				rootNavigationView.Content = platformView;
				_rootView.Content = rootNavigationView;
			}

			if (_disconnected)
			{
				_platformWindow.Activated += OnWindowActivated;
			}

			_disconnected = false;
			_rootView.OnWindowTitleBarContentSizeChanged += WindowRootViewOnWindowTitleBarContentSizeChanged;
		}

		public virtual void Disconnect()
		{
			_rootView.OnWindowTitleBarContentSizeChanged -= WindowRootViewOnWindowTitleBarContentSizeChanged;
			_platformWindow.Activated -= OnWindowActivated;
			SetToolbar(null);
			_rootView.Content = null;
			_disconnected = true;
		}

		internal void SetMenuBar(MenuBar? menuBar)
		{
			_rootView.MenuBar = menuBar;
		}

		internal void SetToolbar(FrameworkElement? toolBar)
		{
			_rootView.Toolbar = toolBar as MauiToolbar;
		}

		internal string? WindowTitle
		{
			get => _rootView.WindowTitle;
			set => _rootView.WindowTitle = value;
		}

		internal void SetTitle(string? title) =>
			_rootView.WindowTitle = title;

		internal void SetTitleBarVisibility(Visibility visibility)
		{
			_rootView.SetTitleBarVisibility(visibility);
		}

		void OnWindowActivated(object sender, WindowActivatedEventArgs e)
		{
			SolidColorBrush defaultForegroundBrush = (SolidColorBrush)Application.Current.Resources["TextFillColorPrimaryBrush"];
			SolidColorBrush inactiveForegroundBrush = (SolidColorBrush)Application.Current.Resources["TextFillColorDisabledBrush"];

			if (e.WindowActivationState == WindowActivationState.Deactivated)
			{
				_rootView.WindowTitleForeground = inactiveForegroundBrush;
			}
			else
			{
				_rootView.WindowTitleForeground = defaultForegroundBrush;
			}
		}
	}
}