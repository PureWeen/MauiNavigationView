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
	public sealed partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();
			NavigationRootManager.Instance = new NavigationRootManager(this);
			this.ExtendsContentIntoTitleBar = true;

			NavigationRootManager.Instance.Connect(new MainPage());

			Content = NavigationRootManager.Instance.RootView;

			var WindowHandle = this.GetWindowHandle();

			// Retrieve current extended style
			var extended_style = PlatformMethods.GetWindowLongPtr(WindowHandle, PlatformMethods.WindowLongFlags.GWL_EXSTYLE);
			long updated_style = extended_style | (long)PlatformMethods.ExtendedWindowStyles.WS_EX_LAYOUTRTL;

			if (updated_style != extended_style)
				PlatformMethods.SetWindowLongPtr(WindowHandle, PlatformMethods.WindowLongFlags.GWL_EXSTYLE, updated_style);
		}
	}
}
