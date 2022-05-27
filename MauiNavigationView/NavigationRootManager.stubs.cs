#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Maui.Platform
{
    public partial class NavigationRootManager
    {
        static NavigationRootManager _instance;
        public static NavigationRootManager Instance { get; set; }
    }
}
