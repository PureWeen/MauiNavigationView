using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Microsoft.Maui.Platform
{
	public partial class TestMauiToolbar : MauiToolbar
	{
		public TestMauiToolbar()
		{
			this.Title = "Welcome to the Maui app";
			this.PrimaryCommands.Add(new AppBarButton() { Content = "App Bar Button" });
		}
	}
}
