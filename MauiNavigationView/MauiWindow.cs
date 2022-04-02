using Microsoft.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiNavigationView
{
    public class MauiWindow : IWindow
    {
        static MauiWindow _instance;
        public static MauiWindow Instance => _instance ??= new MauiWindow();
        public string Title => "HI";

        public void Activated()
        {

        }

        public bool BackButtonClicked()
        {
            return true;
        }

        public void Created()
        {
        }

        public void Deactivated()
        {

        }

        public void Destroying()
        {

        }

        public void DisplayDensityChanged(float displayDensity)
        {

        }

        public void Resumed()
        {

        }

        public void Stopped()
        {

        }
    }
}
