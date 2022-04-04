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
		internal Button? TogglePaneButton { get; private set; }
		internal Button? NavigationViewCloseButton { get; private set; }
		internal ColumnDefinition PaneHeaderCloseButtonColumn { get; private set; }
		internal ColumnDefinition PaneHeaderToggleButtonColumn { get; private set; }

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
			TogglePaneButton = (Button)GetTemplateChild("TogglePaneButton");
			NavigationViewCloseButton = (Button)GetTemplateChild("NavigationViewCloseButton");
			PaneHeaderCloseButtonColumn = (ColumnDefinition)GetTemplateChild("PaneHeaderCloseButtonColumn");
			PaneHeaderToggleButtonColumn = (ColumnDefinition)GetTemplateChild("PaneHeaderToggleButtonColumn");

			UpdateNavigationBackButtonSize();
			UpdateNavigationViewContentMargin();
			UpdateNavigationViewBackButtonMargin();
			UpdateNavigationViewButtonHolderGridMargin();
			OnApplyTemplateCore();
			OnApplyTemplateFinished?.Invoke(this, EventArgs.Empty);

			TogglePaneButton.RegisterPropertyChangedCallback(Button.HeightProperty, (_, __) => UpdatePaneToggleButtonSize());
			TogglePaneButton.RegisterPropertyChangedCallback(Button.WidthProperty, (_, __) => UpdatePaneToggleButtonSize());

			NavigationViewBackButton.RegisterPropertyChangedCallback(Button.HeightProperty, (_, __) => UpdateNavigationBackButtonSize());
			NavigationViewBackButton.RegisterPropertyChangedCallback(Button.WidthProperty, (_, __) => UpdateNavigationBackButtonSize());

			NavigationViewCloseButton.RegisterPropertyChangedCallback(Button.HeightProperty, (_, __) => UpdateNavigationBackButtonSize());
			NavigationViewCloseButton.RegisterPropertyChangedCallback(Button.WidthProperty, (_, __) => UpdateNavigationBackButtonSize());

			PaneHeaderCloseButtonColumn.RegisterPropertyChangedCallback(ColumnDefinition.WidthProperty, (_, __) => PaneHeaderCloseButtonColumn.Width = new GridLength(0));
			PaneHeaderToggleButtonColumn.RegisterPropertyChangedCallback(ColumnDefinition.WidthProperty, (_, __) => PaneHeaderToggleButtonColumn.Width = new GridLength(0));

			PaneHeaderToggleButtonColumn.Width = new GridLength(0);
			PaneHeaderCloseButtonColumn.Width = new GridLength(0);
		}

		private protected virtual void OnApplyTemplateCore()
		{			
		}

		#region Toolbar
		public static readonly DependencyProperty ToolbarProperty
			= DependencyProperty.Register(nameof(Toolbar), typeof(UIElement), typeof(MauiNavigationView),
				new PropertyMetadata(null, (d, _) => ((RootNavigationView)d).ToolbarChanged()));

		public UIElement? Toolbar
		{
			get => (UIElement?)GetValue(ToolbarProperty);
			set => SetValue(ToolbarProperty, value);
		}

		protected private virtual void ToolbarChanged()
		{
			Header = Toolbar;
		}

		#endregion

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

			if (NavigationViewCloseButton != null)
				NavigationViewCloseButton.Margin = NavigationViewBackButtonMargin;
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
				if (NavigationViewBackButton.Height != NavigationBackButtonHeight)
					NavigationViewBackButton.Height = NavigationBackButtonHeight;

				if (NavigationViewBackButton.Width != NavigationBackButtonWidth)
					NavigationViewBackButton.Width = NavigationBackButtonWidth;

				if (NavigationViewCloseButton.Height != NavigationBackButtonHeight)
					NavigationViewCloseButton.Height = NavigationBackButtonHeight;

				if (NavigationViewCloseButton.Width != NavigationBackButtonWidth)
					NavigationViewCloseButton.Width = NavigationBackButtonWidth;
			}
		}
		#endregion

		#region Flyout Custom Content
		public static readonly DependencyProperty FlyoutCustomContentProperty
			= DependencyProperty.Register(nameof(FlyoutCustomContent), typeof(UIElement), typeof(MauiNavigationView),
				new PropertyMetadata(null, (d, _) => ((RootNavigationView)d).UpdateFlyoutCustomContent()));

		public UIElement FlyoutCustomContent
		{
			get => (UIElement)GetValue(FlyoutCustomContentProperty);
			set => SetValue(FlyoutCustomContentProperty, value);
		}

		protected private virtual void UpdateFlyoutCustomContent()
		{
			PaneCustomContent = FlyoutCustomContent;
		}
		#endregion

		#region PaneToggleButton
		internal static double DefaultPaneToggleButtonHeight => (double)Application.Current.Resources["PaneToggleButtonHeight"];

		// The resource is set to 40 but it appears that the NavigationView manually sets the width to 48
		internal static double DefaultPaneToggleButtonWidth => 48;//(double)Application.Current.Resources["PaneToggleButtonWidth"];

		internal static Thickness DefaultPaneToggleButtonPadding => (Thickness)Application.Current.Resources["NavigationViewItemButtonMargin"];

		public static readonly DependencyProperty PaneToggleButtonPaddingProperty
			= DependencyProperty.Register(nameof(PaneToggleButtonPadding), typeof(Thickness), typeof(MauiNavigationView),
				new PropertyMetadata(DefaultPaneToggleButtonPadding, OnPaneToggleButtonSizeChanged));

		public Thickness PaneToggleButtonPadding
		{
			get => (Thickness)GetValue(PaneToggleButtonPaddingProperty);
			set => SetValue(PaneToggleButtonPaddingProperty, value);
		}

		public static readonly DependencyProperty PaneToggleButtonHeightProperty
			= DependencyProperty.Register(nameof(PaneToggleButtonHeight), typeof(double), typeof(MauiNavigationView),
				new PropertyMetadata(DefaultPaneToggleButtonHeight, OnPaneToggleButtonSizeChanged));

		public double PaneToggleButtonHeight
		{
			get => (double)GetValue(PaneToggleButtonHeightProperty);
			set => SetValue(PaneToggleButtonHeightProperty, value);
		}

		public static readonly DependencyProperty PaneToggleButtonWidthProperty
			= DependencyProperty.Register(nameof(PaneToggleButtonWidth), typeof(double), typeof(MauiNavigationView),
				new PropertyMetadata(DefaultPaneToggleButtonWidth, OnPaneToggleButtonSizeChanged));

		public double PaneToggleButtonWidth
		{
			get => (double)GetValue(PaneToggleButtonWidthProperty);
			set => SetValue(PaneToggleButtonWidthProperty, value);
		}

		static void OnPaneToggleButtonSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MauiNavigationView)d).UpdatePaneToggleButtonSize();
		}


		void UpdatePaneToggleButtonSize()
		{
			if (TogglePaneButton != null)
			{
				if (PaneToggleButtonHeight != TogglePaneButton.Height)
				{
					TogglePaneButton.Height = PaneToggleButtonHeight;

					var togglePaneButtonMinHeight = Math.Min((double)Application.Current.Resources["PaneToggleButtonHeight"], PaneToggleButtonHeight);
					if (TogglePaneButton.MinHeight != PaneToggleButtonHeight)
						TogglePaneButton.MinHeight = PaneToggleButtonHeight;

					if (TogglePaneButton.GetFirstDescendant<Grid>() is Grid grid)
					{
						if (grid.Height != PaneToggleButtonHeight)
							grid.Height = PaneToggleButtonHeight;

						// The row definition is bound to PaneToggleButtonHeight
						// the height is bound to MinHeight of the button
						if (grid.RowDefinitions[0].Height.Value != PaneToggleButtonHeight)
							grid.RowDefinitions[0].Height = new GridLength(PaneToggleButtonHeight);
					}
				}

				if (PaneToggleButtonWidth != TogglePaneButton.Width)
					TogglePaneButton.Width = PaneToggleButtonWidth;

				var togglePaneButtonMinWidth = Math.Min((double)Application.Current.Resources["PaneToggleButtonWidth"], PaneToggleButtonWidth);
				if (TogglePaneButton.MinWidth != togglePaneButtonMinWidth)
					TogglePaneButton.MinWidth = togglePaneButtonMinWidth;

				if (TogglePaneButton.Padding != PaneToggleButtonPadding)
					TogglePaneButton.Padding = PaneToggleButtonPadding;
			}
		}
		#endregion
	}
}
