using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SortDesktopIcon
{
    class IcoObj
    {
        public string name;
        public IntPtr itemIntPtr;
        public Point itemPoint;

        public IcoObj(string vname, IntPtr vintptr, Point vpoint)
        {
            name = vname;
            itemIntPtr = vintptr;
            itemPoint = vpoint;
        }
    }
}
