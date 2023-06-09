using MauiNavigationView;
using System;
using System.Threading.Tasks;
using Windows.UI;
using WinRT.Interop;

namespace Microsoft.Maui.Platform
{
	public static partial class WindowExtensions
	{
		//public static void UpdateTitle(this UI.Xaml.Window platformWindow, IWindow window)
		//{
		//	platformWindow.Title = window.Title;
		//	mauiContext?
		//		.GetNavigationRootManager()?
		//		.SetTitle(window.Title);
		//}

		public static IWindow? GetWindow(this UI.Xaml.Window platformWindow)
		{
			return MauiWindow.Instance;
		}

		public static IntPtr GetWindowHandle(this UI.Xaml.Window platformWindow)
		{
			var hwnd = WindowNative.GetWindowHandle(platformWindow);

			if (hwnd == IntPtr.Zero)
				throw new NullReferenceException("The Window Handle is null.");

			return hwnd;
		}

		public static float GetDisplayDensity(this UI.Xaml.Window platformWindow)
		{
			var hwnd = platformWindow.GetWindowHandle();

			if (hwnd == IntPtr.Zero)
				return 1.0f;

			return PlatformMethods.GetDpiForWindow(hwnd) / 96f;
		}

		public static UI.Windowing.AppWindow? GetAppWindow(this UI.Xaml.Window platformWindow)
		{
			var hwnd = platformWindow.GetWindowHandle();

			if (hwnd == IntPtr.Zero)
				return null;

			var windowId = UI.Win32Interop.GetWindowIdFromWindow(hwnd);
			return UI.Windowing.AppWindow.GetFromWindowId(windowId);
		}
	}
}