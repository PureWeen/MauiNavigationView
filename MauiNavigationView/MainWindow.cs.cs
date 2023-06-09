using Microsoft.Maui;
using Microsoft.Maui.Platform;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiNavigationView
{
	/// <summary>
	/// An empty window that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainWindow : MauiWinUIWindow
	{
		public MainWindow()
		{
			NavigationRootManager.Instance = new NavigationRootManager(this);

			NavigationRootManager.Instance.Connect(new MainPage());

			//NavigationRootManager.Instance.Connect(
			//	new Microsoft.Maui.Platform.MauiNavigationView()
			//	{
			//		Content = new TextBlock() { Text = "CONTENT" },
			//		PaneDisplayMode = NavigationViewPaneDisplayMode.Top,
			//		IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed,
			//		IsSettingsVisible = false,
			//		IsPaneToggleButtonVisible = false
			//	});

			Content = NavigationRootManager.Instance.RootView;


			var mauiRootView = NavigationRootManager.Instance.RootView as WindowRootView;
			mauiRootView.Toolbar = new TestMauiToolbar();
			if (Content is WindowRootView wrv && wrv.Content is RootNavigationView rnv)
				rnv.Loaded += RootView_Loaded;
		}

		private void RootView_Loaded(object sender, RoutedEventArgs e)
		{
			if (sender is RootNavigationView rnv)
			{
				rnv.MenuItems.Add(new NavigationViewItem() { Content = "asdf" });
				rnv.MenuItems.Add(new NavigationViewItem() { Content = "asdf" });
				rnv.MenuItems.Add(new NavigationViewItem() { Content = "asdf" });
			}
		}
	}
}
