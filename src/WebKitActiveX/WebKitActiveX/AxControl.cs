using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Awesomium.Windows.Forms;
using System.Windows.Controls;
using System.Runtime.InteropServices;

namespace WebKitActiveX
{
    public class AxControl : WebControl
    {
        public AxControl()
        {
            Dock = System.Windows.Forms.DockStyle.Fill;
            InjectMouseDown(Awesomium.Core.MouseButton.Left);
        }
    }
}
