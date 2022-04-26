using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Microsoft.Maui.Platform
{
	public partial class ShellItemView : MauiNavigationView
	{
		public ShellItemView()
		{
			InitializeComponent();
			PaneDisplayMode = NavigationViewPaneDisplayMode.Top;
			IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
			IsSettingsVisible = false;
			IsPaneToggleButtonVisible = false;
			MenuItemTemplate = (UI.Xaml.DataTemplate)Application.Current.Resources["TabBarNavigationViewMenuItem"];


			this.SetApplicationResource("NavigationViewMinimalHeaderMargin", null);
			this.SetApplicationResource("NavigationViewHeaderMargin", null);
			this.SetApplicationResource("NavigationViewMinimalContentGridBorderThickness", null);
		}
	}
}
