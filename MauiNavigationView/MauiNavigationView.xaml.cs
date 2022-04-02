using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Windows.Foundation;
using WBrush = Microsoft.UI.Xaml.Media.Brush;
using WRectangle = Microsoft.UI.Xaml.Shapes.Rectangle;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Media;

namespace Microsoft.Maui.Platform
{
	// This is needed by WinUI because of 
	// https://github.com/microsoft/microsoft-ui-xaml/issues/2698#issuecomment-648751713
	[Microsoft.UI.Xaml.Data.Bindable]
	public partial class MauiNavigationView : NavigationView
	{
		internal StackPanel? TopNavArea { get; private set; }
		internal ItemsRepeater? TopNavMenuItemsHost { get; private set; }
		internal Grid? PaneContentGrid { get; private set; }
		internal event EventHandler? OnApplyTemplateFinished;
		internal SplitView? RootSplitView { get; private set; }
		internal ScrollViewer? MenuItemsScrollViewer { get; private set; }
		internal Grid? ContentPaneTopPadding { get; private set; }
		internal Grid? PaneToggleButtonGrid { get; private set; }
		internal Grid? ButtonHolderGrid { get; private set; }
		internal Grid? ContentGrid { get; private set; }
		internal Button? NavigationViewBackButton { get; private set; }

		public MauiNavigationView()
		{
			InitializeComponent();
		}

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			MenuItemsScrollViewer = (ScrollViewer)GetTemplateChild("MenuItemsScrollViewer");
			PaneContentGrid = (Grid)GetTemplateChild("PaneContentGrid");
			RootSplitView = (SplitView)GetTemplateChild("RootSplitView");
			TopNavArea = ((StackPanel)GetTemplateChild("TopNavArea"));
			TopNavMenuItemsHost = ((ItemsRepeater)GetTemplateChild("TopNavMenuItemsHost"));
			ContentPaneTopPadding = (Grid)GetTemplateChild("ContentPaneTopPadding");
			PaneToggleButtonGrid = (Grid)GetTemplateChild("PaneToggleButtonGrid");
			ButtonHolderGrid = (Grid)GetTemplateChild("ButtonHolderGrid");
			ContentGrid = (Grid)GetTemplateChild("ContentGrid");
			NavigationViewBackButton = (Button)GetTemplateChild("NavigationViewBackButton");
			UpdateNavigationBackButtonSize();
			UpdateNavigationViewContentMargin();
			UpdateNavigationViewBackButtonMargin();
			UpdateNavigationViewButtonHolderGridMargin();
			OnApplyTemplateCore();
			OnApplyTemplateFinished?.Invoke(this, EventArgs.Empty);
		}

		private protected virtual void OnApplyTemplateCore()
		{

		}


		#region NavigationViewBackButtonMargin
		public static readonly DependencyProperty NavigationViewBackButtonMarginProperty
			= DependencyProperty.Register(nameof(NavigationViewBackButtonMargin), typeof(Thickness), typeof(MauiNavigationView),
				new PropertyMetadata(new Thickness(4, 2, 0, 2), NavigationViewBackButtonMarginChanged));

		public Thickness NavigationViewBackButtonMargin
		{
			get => (Thickness)GetValue(NavigationViewBackButtonMarginProperty);
			set => SetValue(NavigationViewBackButtonMarginProperty, value);
		}

		static void NavigationViewBackButtonMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MauiNavigationView)d).UpdateNavigationViewBackButtonMargin();
		}

		void UpdateNavigationViewBackButtonMargin()
		{
			if (NavigationViewBackButton != null)
				NavigationViewBackButton.Margin = NavigationViewBackButtonMargin;
		}
		#endregion


		#region NavigationViewButtonHolderGridMargin
		public static readonly DependencyProperty NavigationViewButtonHolderGridMarginProperty
			= DependencyProperty.Register(nameof(NavigationViewButtonHolderGridMargin), typeof(Thickness), typeof(MauiNavigationView),
				new PropertyMetadata((Thickness)Application.Current.Resources["NavigationViewButtonHolderGridMargin"], NavigationViewButtonHolderGridMarginChanged));

		public Thickness NavigationViewButtonHolderGridMargin
		{
			get => (Thickness)GetValue(NavigationViewButtonHolderGridMarginProperty);
			set => SetValue(NavigationViewButtonHolderGridMarginProperty, value);
		}

		static void NavigationViewButtonHolderGridMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MauiNavigationView)d).UpdateNavigationViewButtonHolderGridMargin();
		}

		void UpdateNavigationViewButtonHolderGridMargin()
		{
			if (ButtonHolderGrid != null)
				ButtonHolderGrid.Margin = NavigationViewButtonHolderGridMargin;
		}
		#endregion

		#region NavigationViewContentMargin
		public static readonly DependencyProperty NavigationViewContentMarginProperty
			= DependencyProperty.Register(nameof(NavigationViewContentMargin), typeof(Thickness), typeof(MauiNavigationView),
				new PropertyMetadata(new Thickness(), OnNavigationViewContentMarginChanged));

		public Thickness NavigationViewContentMargin
		{
			get => (Thickness)GetValue(NavigationViewContentMarginProperty);
			set => SetValue(NavigationViewContentMarginProperty, value);
		}

		static void OnNavigationViewContentMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MauiNavigationView)d).UpdateNavigationViewContentMargin();
		}

		void UpdateNavigationViewContentMargin()
		{
			if (ContentGrid != null)
				ContentGrid.Margin = NavigationViewContentMargin;
		}
		#endregion

		#region NavigationBackButtonHeight/Width
		internal static double DefaultNavigationBackButtonHeight => (double)Application.Current.Resources["NavigationBackButtonHeight"];
		internal static double DefaultNavigationBackButtonWidth => (double)Application.Current.Resources["NavigationBackButtonWidth"];

		public static readonly DependencyProperty NavigationBackButtonHeightProperty
			= DependencyProperty.Register(nameof(NavigationBackButtonHeight), typeof(double), typeof(MauiNavigationView),
				new PropertyMetadata(DefaultNavigationBackButtonHeight, OnNavigationBackButtonSizeChanged));

		public double NavigationBackButtonHeight
		{
			get => (double)GetValue(NavigationBackButtonHeightProperty);
			set => SetValue(NavigationBackButtonHeightProperty, value);
		}

		public static readonly DependencyProperty NavigationBackButtonWidthProperty
			= DependencyProperty.Register(nameof(NavigationBackButtonWidth), typeof(double), typeof(MauiNavigationView),
				new PropertyMetadata(DefaultNavigationBackButtonWidth, OnNavigationBackButtonSizeChanged));

		public double NavigationBackButtonWidth
		{
			get => (double)GetValue(NavigationBackButtonWidthProperty);
			set => SetValue(NavigationBackButtonWidthProperty, value);
		}

		static void OnNavigationBackButtonSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MauiNavigationView)d).UpdateNavigationBackButtonSize();
		}


		void UpdateNavigationBackButtonSize()
		{
			if (NavigationViewBackButton != null)
			{
				NavigationViewBackButton.Height = NavigationBackButtonHeight;
				NavigationViewBackButton.Width = NavigationBackButtonWidth;
			}
		}
		#endregion
	}
}
