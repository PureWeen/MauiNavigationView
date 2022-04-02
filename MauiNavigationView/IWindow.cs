using System.Collections.Generic;

namespace Microsoft.Maui
{
	/// <summary>
	/// Provides the ability to create, configure, show, and manage Windows.
	/// </summary>
	public interface IWindow
	{
		/// <summary>
		/// Occurs when the Window is created.
		/// </summary>
		void Created();

		/// <summary>
		/// Occurs when the Window is resumed from a sleeping state.
		/// </summary>
		void Resumed();

		/// <summary>
		/// Occurs when the Window is activated.
		/// </summary>
		void Activated();

		/// <summary>
		/// Occurs when the Window is deactivated.
		/// </summary>
		void Deactivated();

		/// <summary>
		/// Occurs when the Window is stopped.
		/// </summary>
		void Stopped();

		/// <summary>
		/// Occurs when the Window is destroyed.
		/// </summary>
		void Destroying();

		/// <summary>
		/// Occurs when the back button is pressed.
		/// </summary>
		/// <returns>Whether or not the back navigation was handled.</returns>
		bool BackButtonClicked();

		void DisplayDensityChanged(float displayDensity);

		string Title { get; }
	}
}