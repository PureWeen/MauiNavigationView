using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Maui.Graphics
{
    [DebuggerDisplay("X={X}, Y={Y}, Width={Width}, Height={Height}")]
    public struct Rect
    {
        public Rect()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }

        public Rect(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Rect Zero;

        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Top
        {
            get
            {
                return Y;
            }
            set
            {
                Y = value;
            }
        }

        public double Bottom
        {
            get
            {
                return Y + Height;
            }
            set
            {
                Height = value - Y;
            }
        }

        public double Right
        {
            get
            {
                return X + Width;
            }
            set
            {
                Width = value - X;
            }
        }

        public double Left
        {
            get
            {
                return X;
            }
            set
            {
                X = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (!(Width <= 0.0))
                {
                    return Height <= 0.0;
                }

                return true;
            }
        }
    }
}