#nullable enable
using System;
using System.Runtime.InteropServices;
using Microsoft.Maui.Platform;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Controls;

namespace Microsoft.Maui
{
	public class MauiWinUIWindow : UI.Xaml.Window
	{
		IntPtr _windowIcon;
		bool _enableResumeEvent;

		public MauiWinUIWindow()
		{
			Closed += OnClosedPrivate;
			// We set this to true by default so later on if it's
			// set to false we know the user toggled this to false 
			// and then we can react accordingly
			ExtendsContentIntoTitleBar = true;
		}


		private void OnClosedPrivate(object sender, UI.Xaml.WindowEventArgs args)
		{
			if (_windowIcon != IntPtr.Zero)
			{
				DestroyIcon(_windowIcon);
				_windowIcon = IntPtr.Zero;
			}
		}

		UI.Xaml.UIElement? _customTitleBar;
		internal UI.Xaml.UIElement? MauiCustomTitleBar
		{
			get => _customTitleBar;
			set
			{
				_customTitleBar = value;
				SetTitleBar(_customTitleBar);
				UpdateTitleOnCustomTitleBar();
			}
		}

		internal void UpdateTitleOnCustomTitleBar()
		{
			if (_customTitleBar is UI.Xaml.FrameworkElement fe &&
				fe.GetDescendantByName<TextBlock>("AppTitle") is TextBlock tb)
			{
				tb.Text = Title;
			}
		}


		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, string iconPath, ref IntPtr index);

		[DllImport("user32.dll", SetLastError = true)]
		static extern int DestroyIcon(IntPtr hIcon);
	}
}
